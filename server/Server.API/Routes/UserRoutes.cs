namespace Server.API.Routes;

/// <summary>
/// User routes
/// </summary>
public static class UserRoutes
{
  /// <summary>
  /// Maps user endpoints
  /// </summary>
  public static void MapUsersEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("users");

    group
      .MapGet("{id}", UsersController.GetUserAsync)
      .RequireAuthorization()
      .WithName("GetUser")
      .WithDescription("Get a user by id");
  }
}