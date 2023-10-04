namespace Server.API.Data;

/// <summary>
/// Options for interacting with MongoDB
/// </summary>
class MongoDbOptions
{
  /// <summary>
  /// The connection string
  /// </summary>
  public string ConnectionString { get; set; } = string.Empty;

  /// <summary>
  /// The database name
  /// </summary>
  public string DatabaseName { get; set; } = string.Empty;
}

/// <summary>
/// Configures <see cref="MongoDbOptions"/>
/// </summary>
class MongoDbOptionsSetup : IConfigureOptions<MongoDbOptions>
{
  private const string SectionName = nameof(MongoDbOptions);
  private readonly IConfiguration _configuration;

  /// <summary>
  /// Creates a new <see cref="MongoDbOptionsSetup"/> instance
  /// </summary>
  /// <param name="configuration">A <see cref="IConfiguration"/> instance</param>
  /// <returns>A <see cref="MongoDbOptionsSetup"/> instance</returns>
  public MongoDbOptionsSetup(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  /// <summary>
  /// Configures <see cref="MongoDbOptions"/>
  /// </summary>
  /// <param name="options">A <see cref="MongoDbOptions"/> instance</param>
  /// <returns>A <see cref="MongoDbOptions"/> instance</returns>
  public void Configure(MongoDbOptions options)
  {
    _configuration
      .GetSection(SectionName)
      .Bind(options);
  }
}