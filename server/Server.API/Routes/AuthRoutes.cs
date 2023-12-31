namespace Server.API.Routes;

/// <summary>
/// Auth routes
/// </summary>
public static class AuthRoutes
{
  /// <summary>
  /// Maps auth endpoints
  /// </summary>
  public static void MapAuthEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("auth");

    group
      .MapPost("register", AuthController.RegisterAsync)
      .WithName("RegisterUser")
      .WithDescription("Register a new user");

    group
      .MapPost("login", AuthController.LoginAsync)
      .WithName("LoginUser")
      .WithDescription("Login a user");

    group
      .MapPost("logout", AuthController.LogoutAsync)
      .RequireAuthorization()
      .WithName("LogoutUser")
      .WithDescription("Logout a user");

    group
      .MapPost("refresh-token", AuthController.RefreshTokenAsync)
      .WithName("RefreshToken")
      .WithDescription("Refresh a user's token");
  }
}