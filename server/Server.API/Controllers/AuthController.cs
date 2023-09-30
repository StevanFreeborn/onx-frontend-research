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
    var registerResult = await req.UserService.RegisterUser(newUser);

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
    return await Task.FromResult(Results.Ok(req));
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