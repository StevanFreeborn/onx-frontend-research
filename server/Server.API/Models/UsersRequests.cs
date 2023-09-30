namespace Server.API.Models;

record GetUserRequest(
  [FromRoute] string Id,
  [FromServices] IUserService UserService
);