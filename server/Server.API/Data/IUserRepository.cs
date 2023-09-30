namespace Server.API.Data;

interface IUserRepository
{
  Task<User?> GetUserByEmailAsync(string email);
  Task<User> CreateUserAsync(User newUser);
  Task<User?> GetUserByIdAsync(string id);
  Task<User?> GetUserByUsernameAsync(string username);
}