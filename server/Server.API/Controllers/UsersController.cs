namespace Server.API.Controllers;

static class UsersController
{
  internal static async Task<IResult> GetUserAsync([AsParameters] GetUserRequest req)
  {
    var userResult = await req.UserService.GetUserAsync(req.Id);

    if (
      userResult.IsFailed &&
      userResult.Errors.OfType<UserNotFoundError>().Any()
    )
    {
      var error = userResult.Errors.OfType<UserNotFoundError>().First();
      return Results.Problem(
        title: "Unable to get user",
        detail: error.Message,
        statusCode: 404
      );
    }

    return Results.Ok(new GetUserResponse(userResult.Value.Id, userResult.Value.Email));
  }
}