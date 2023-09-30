namespace Server.API.Data;

public static class MongoClassMapper
{
  public static void RegisterClassMappings()
  {
    BsonClassMap.RegisterClassMap<User>(
      cm =>
      {
        cm.AutoMap();
        cm.MapIdProperty(u => u.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
        cm.MapProperty(u => u.Username).SetElementName("username");
        cm.MapProperty(u => u.Email).SetElementName("email");
        cm.MapProperty(u => u.Password).SetElementName("password");
        cm.MapProperty(u => u.CreatedAt).SetElementName("createdAt");
        cm.MapProperty(u => u.UpdatedAt).SetElementName("updatedAt");
      }
    );
  }
}