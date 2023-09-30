
namespace Server.API.Data;

class MongoUserRepository : IUserRepository
{
  private readonly MongoDbContext _context;

  internal MongoUserRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetUserByEmailAsync(string email)
  {
    return await _context.Users
      .Find(u => u.Email == email)
      .FirstOrDefaultAsync();
  }
}