namespace Server.API.Controllers;

static class AuthController
{
  internal static async Task<IResult> LoginAsync([AsParameters] LoginRequest req)
  {
    return await Task.FromResult(Results.Ok(req));
  }

  internal static async Task<IResult> LogoutAsync()
  {
    return await Task.FromResult(Results.Ok("logout"));
  }

  internal static async Task<IResult> RefreshTokenAsync()
  {
    return await Task.FromResult(Results.Ok("refresh"));
  }
}