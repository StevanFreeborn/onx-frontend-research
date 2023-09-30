namespace Server.API.Data;

class MongoDbOptions
{
  public string ConnectionString { get; init; } = string.Empty;
  public string DatabaseName { get; init; } = string.Empty;
}

class MongoDbOptionsSetup : IConfigureOptions<MongoDbOptions>
{
  private const string SectionName = nameof(MongoDbOptions);
  private readonly IConfiguration _configuration;

  public MongoDbOptionsSetup(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public void Configure(MongoDbOptions options)
  {
    _configuration
      .GetSection(SectionName)
      .Bind(options);
  }
}