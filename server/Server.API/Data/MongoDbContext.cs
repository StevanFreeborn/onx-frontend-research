namespace Server.API.Data;

class MongoDbContext
{
  private const string UsersCollectionName = "users";
  private readonly MongoDbOptions _options;
  public IMongoCollection<User> Users { get; set; }

  internal MongoDbContext(IOptions<MongoDbOptions> options)
  {
    _options = options.Value;
    var client = new MongoClient(_options.ConnectionString);
    var database = client.GetDatabase(_options.DatabaseName);
    Users = database.GetCollection<User>(UsersCollectionName);
  }
}