namespace Server.API.Models;

/// <summary>
/// Represents a request to retrieve a user
/// </summary>
/// <param name="Id">The user's id</param>
record GetUserRequest(
  [FromRoute] string Id,
  [FromServices] IUserService UserService
);