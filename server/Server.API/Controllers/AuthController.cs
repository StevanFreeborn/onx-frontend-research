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

    req.Context.Response.SetRefreshTokenCookie(
      loginResult.Value.RefreshToken.Token,
      loginResult.Value.RefreshToken.ExpiresAt
    );

    return Results.Ok(new LoginUserResponse(loginResult.Value.AccessToken));
  }

  internal static async Task<IResult> LogoutAsync([AsParameters] LogoutRequest req)
  {
    var userId = req.Context.GetUserId();

    if (userId is null)
    {
      return Results.Problem(
        title: "Unable to logout user",
        detail: "User is not logged in",
        statusCode: 400
      );
    }

    var refreshToken = req.Context.Request.GetRefreshTokenCookie();

    if (string.IsNullOrWhiteSpace(refreshToken) == false)
    {
      await req.UserService.RevokeRefreshTokenAsync(userId, refreshToken);
      await req.UserService.RemoveAllInvalidRefreshTokensAsync(userId);
    }

    req.Context.Response.SetRefreshTokenCookie(string.Empty, DateTime.UtcNow.AddDays(-1));
    return Results.Ok();
  }

  internal static async Task<IResult> RefreshTokenAsync([AsParameters] RefreshTokenRequest req)
  {
    var refreshToken = req.Context.Request.GetRefreshTokenCookie();

    if (string.IsNullOrWhiteSpace(refreshToken))
    {
      return Results.Problem(
        title: "Unable to refresh token",
        detail: "Refresh token is missing",
        statusCode: 400
      );
    }

    var validationResult = await req.Validator.ValidateAsync(req.Dto);

    if (validationResult.IsValid == false)
    {
      return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var refreshResult = await req.UserService.RefreshTokenAsync(req.Dto.UserId, refreshToken);

    if (
      refreshResult.IsFailed &&
      refreshResult.Errors.OfType<InvalidRefreshToken>().Any()
    )
    {
      var error = refreshResult.Errors.OfType<InvalidRefreshToken>().First();

      req.Context.Response.SetRefreshTokenCookie(string.Empty, DateTime.UtcNow.AddDays(-1));

      return Results.Problem(
        title: "Unable to refresh token",
        detail: error.Message,
        statusCode: 400
      );
    }

    if (
      refreshResult.IsFailed &&
      refreshResult.Errors.OfType<RefreshTokenError>().Any()
    )
    {
      var error = refreshResult.Errors.OfType<RefreshTokenError>().First();
      return Results.Problem(
        title: "Unable to refresh token",
        detail: error.Message,
        statusCode: 500
      );
    }

    await req.UserService.RevokeRefreshTokenAsync(req.Dto.UserId, refreshToken);
    await req.UserService.RemoveAllInvalidRefreshTokensAsync(req.Dto.UserId);

    req.Context.Response.SetRefreshTokenCookie(
      refreshResult.Value.RefreshToken.Token,
      refreshResult.Value.RefreshToken.ExpiresAt
    );

    return Results.Ok(new LoginUserResponse(refreshResult.Value.AccessToken));
  }
}