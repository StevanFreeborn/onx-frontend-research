namespace Server.API.Routes;

public static class UserRoutes
{
  public static void MapUsersEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("users");

    group
      .MapGet("{id}", UsersController.GetUserAsync)
      .WithName("GetUser")
      .WithDescription("Get a user by id");
  }
}