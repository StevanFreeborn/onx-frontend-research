using Server.API.Controllers;

namespace Server.API.Routes;

public static class AuthRoutes
{
  public static void MapAuthEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("auth");
    group.MapPost("register", AuthController.RegisterAsync);
    group.MapPost("login", AuthController.LoginAsync);
    group.MapPost("logout", AuthController.LogoutAsync);
    group.MapPost("refresh-token", AuthController.RefreshTokenAsync);
  }
}