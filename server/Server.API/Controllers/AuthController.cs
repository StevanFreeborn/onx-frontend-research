namespace Server.API.Controllers;

static class AuthController
{
  internal static async Task<IResult> RegisterAsync([AsParameters] RegisterRequest req)
  {
    var validationResult = await req.Validator.ValidateAsync(req.Dto);

    if (validationResult.IsValid == false)
    {
      return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var newUser = req.Dto.ToUser();
    var registerResult = await req.UserService.RegisterUserAsync(newUser);

    if (
      registerResult.IsFailed &&
      registerResult.Errors.OfType<UserAlreadyExistError>().Any()
    )
    {
      var error = registerResult.Errors.OfType<UserAlreadyExistError>().First();
      return Results.Problem(
        title: "Unable to register user",
        detail: error.Message,
        statusCode: 400
      );
    }

    return Results.Created(
      uri: $"/users/{registerResult.Value}",
      value: new RegisterUserResponse(registerResult.Value)
    );
  }

  internal static async Task<IResult> LoginAsync([AsParameters] LoginRequest req)
  {
    var validationResult = await req.Validator.ValidateAsync(req.Dto);

    if (validationResult.IsValid == false)
    {
      return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var loginResult = await req.UserService.LoginUserAsync(req.Dto.Username, req.Dto.Password);

    if (
      loginResult.IsFailed &&
      loginResult.Errors.OfType<InvalidLoginError>().Any()
    )
    {
      var error = loginResult.Errors.OfType<InvalidLoginError>().First();
      return Results.Problem(
        title: "Unable to login user",
        detail: error.Message,
        statusCode: 404
      );
    }

    // TODO: set refresh token cookie

    return Results.Ok(new LoginUserResponse(loginResult.Value.AccessToken));
  }

  internal static async Task<IResult> LogoutAsync()
  {
    return await Task.FromResult(Results.Ok("logout"));
  }

  internal static async Task<IResult> RefreshTokenAsync()
  {
    return await Task.FromResult(Results.Ok("refresh"));
  }
}