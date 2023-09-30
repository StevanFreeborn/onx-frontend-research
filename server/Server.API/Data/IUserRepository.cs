namespace Server.API.Data;

interface IUserRepository
{
  Task<User?> GetUserByEmailAsync(string email);
}