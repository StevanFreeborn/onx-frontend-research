namespace Client.Services;

record LoginRequest(string Email, string Password);
record LoginResponse(string Token);

interface IAuthServiceClient
{
  Task<HttpResponseMessage> LoginAsync(LoginRequest request);
}

class AuthServiceClient : IAuthServiceClient
{
  private readonly HttpClient _httpClient;

  public AuthServiceClient(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<HttpResponseMessage> LoginAsync(LoginRequest request)
  {
    return await _httpClient.PostAsJsonAsync("auth/login", request);
  }
}


static class AuthServiceClientExtensions
{
  internal static IServiceCollection AddAuthServiceClient(this IServiceCollection services)
  {
    services.AddHttpClient<IAuthServiceClient, AuthServiceClient>(
      (sp, client) =>
      {
        var baseAddress = sp.GetRequiredService<IOptions<ServerApiOptions>>().Value.ServerApiBaseUrl;
        client.BaseAddress = new Uri(baseAddress);
      }
    );

    return services;
  }
}

interface IAuthService
{
  Task<string> LoginAsync(string email, string password);
}

class AuthService : IAuthService
{
  private readonly IAuthServiceClient _authServiceClient;

  public AuthService(IAuthServiceClient authServiceClient)
  {
    _authServiceClient = authServiceClient;
  }

  public async Task<string> LoginAsync(string email, string password)
  {
    var response = await _authServiceClient.LoginAsync(new LoginRequest(email, password));

    if (response.IsSuccessStatusCode is false)
    {
      return string.Empty;
    }

    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

    return loginResponse?.Token ?? string.Empty;
  }
}