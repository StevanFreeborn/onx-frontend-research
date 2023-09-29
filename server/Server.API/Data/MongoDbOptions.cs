namespace Server.API.Data;

class MongoDbOptions
{
  public string ConnectionString { get; init; } = string.Empty;
  public string DatabaseName { get; init; } = string.Empty;
}