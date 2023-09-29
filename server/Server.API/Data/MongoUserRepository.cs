namespace Server.API.Data;

class MongoUserRepository : IUserRepository
{
  private readonly MongoDbContext _context;

  internal MongoUserRepository(MongoDbContext context)
  {
    _context = context;
  }
}