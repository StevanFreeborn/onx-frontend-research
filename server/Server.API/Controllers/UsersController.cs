namespace Server.API.Controllers;

/// <summary>
/// Controller for handling user requests
/// </summary>
static class UsersController
{
  /// <summary>
  /// Gets a user
  /// </summary>
  /// <param name="req">A <see cref="GetUserRequest"/> instance</param>
  /// <returns>
  /// If the user was found, a <see cref="GetUserResponse"/> instance is returned with a 200 status code.
  /// If the user was not found, a <see cref="ProblemDetails"/> instance is returned with a 404 status code.
  /// </returns>
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