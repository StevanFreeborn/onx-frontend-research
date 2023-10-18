namespace Client.Services;

public class ServerApiOptions
{
  public string ServerApiBaseUrl { get; set; } = string.Empty;
}

class ServerApiOptionsSetup : IConfigureOptions<ServerApiOptions>
{
  private const string SectionName = nameof(ServerApiOptions);
  private readonly IConfiguration _configuration;

  public ServerApiOptionsSetup(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public void Configure(ServerApiOptions options)
  {
    _configuration
      .GetSection(SectionName)
      .Bind(options);
  }
}