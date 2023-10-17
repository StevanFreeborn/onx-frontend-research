namespace Client.Providers;

class AuthStateProvider : AuthenticationStateProvider
{
  public override Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    // Get jwt token from local storage
    // Check if jwt token is expired
    // if expired, attempt to refresh token
    // if refresh token fails, return anonymous user
    // if refresh token succeeds, update local storage, and return authenticated user
    // Build identity from jwt token
    var identity = new ClaimsIdentity();

    var user = new ClaimsPrincipal(identity);
    var state = new AuthenticationState(user);
    var task = Task.FromResult(state);

    NotifyAuthenticationStateChanged(task);

    return task;
  }
}