
namespace Server.API.Data;

class MongoUserRepository : IUserRepository
{
  private readonly MongoDbContext _context;

  public MongoUserRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task<User> CreateUserAsync(User newUser)
  {
    await _context.Users.InsertOneAsync(newUser);
    return newUser;
  }

  public async Task<User?> GetUserByEmailAsync(string email)
  {
    return await _context.Users
      .Find(u => u.Email == email)
      .FirstOrDefaultAsync();
  }

  public async Task<User?> GetUserByIdAsync(string id)
  {
    return await _context.Users
      .Find(u => u.Id == id)
      .FirstOrDefaultAsync();
  }

  public async Task<User?> GetUserByUsernameAsync(string username)
  {
    return await _context.Users
      .Find(u => u.Username == username)
      .FirstOrDefaultAsync();
  }
}