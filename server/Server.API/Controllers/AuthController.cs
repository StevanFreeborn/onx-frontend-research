namespace Server.API.Controllers;

static class AuthController
{
  private static readonly string RefreshCookieName = "onxRefreshToken";

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
    var refreshToken = req.Context.Request.GetRefreshTokenCookie();

    if (string.IsNullOrWhiteSpace(refreshToken) == false)
    {
      // TODO: implement the following
      // if there is a refresh token
      // we need to
      // 1. revoke the refresh token
      // 3. remove any expired and revoked refresh tokens from the user
      // var revokeResult = await req.UserService.RevokeRefreshTokenAsync(refreshToken);
    }

    req.Context.Response.SetRefreshTokenCookie(string.Empty, DateTime.UtcNow.AddDays(-1));
    return await Task.FromResult(Results.Ok("logout"));
  }

  internal static async Task<IResult> RefreshTokenAsync()
  {
    return await Task.FromResult(Results.Ok("refresh"));
  }

  private static void SetRefreshTokenCookie(this HttpResponse response, string token, DateTime expiresAt)
  {
    response.Cookies.Append(
      RefreshCookieName,
      token,
      new CookieOptions
      {
        HttpOnly = true,
        Expires = expiresAt,
        SameSite = SameSiteMode.None,
        Secure = true
      }
    );
  }

  private static string? GetRefreshTokenCookie(this HttpRequest request)
  {
    return request.Cookies[RefreshCookieName];
  }
}