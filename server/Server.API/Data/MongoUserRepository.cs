
namespace Server.API.Data;

/// <summary>
///  Repository for handling user data
/// </summary>
/// <inheritdoc/>
class MongoUserRepository : IUserRepository
{
  private readonly MongoDbContext _context;

  /// <summary>
  /// Creates a new <see cref="MongoUserRepository"/> instance
  /// </summary>
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

  public async Task<User?> UpdateUserAsync(User user)
  {
    var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
    var options = new FindOneAndReplaceOptions<User, User>
    {
      ReturnDocument = ReturnDocument.After
    };

    user.UpdatedAt = DateTime.UtcNow;

    return await _context.Users.FindOneAndReplaceAsync(filter, user, options);
  }
}