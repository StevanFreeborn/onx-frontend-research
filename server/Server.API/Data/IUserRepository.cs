namespace Server.API.Data;

/// <summary>
/// Repository for handling user data
/// </summary>
interface IUserRepository
{
  /// <summary>
  /// Gets a user by email
  /// </summary>
  /// <param name="email">The user's email</param>
  /// <returns>the user as a <see cref="User"/> instance</returns>
  Task<User?> GetUserByEmailAsync(string email);

  /// <summary>
  /// Creates a new user
  /// </summary>
  /// <param name="newUser">A <see cref="User"/> instance</param>
  /// <returns>The new user as a <see cref="User"/> instance</returns>
  Task<User> CreateUserAsync(User newUser);

  /// <summary>
  /// Gets a user by id
  /// </summary>
  /// <param name="id">The user's id</param>
  /// <returns>The user</returns>
  Task<User?> GetUserByIdAsync(string id);

  /// <summary>
  /// Gets a user by username
  /// </summary>
  /// <param name="username">The user's username</param>
  /// <returns>The user as a <see cref="User"/> instance</returns>
  Task<User?> GetUserByUsernameAsync(string username);

  /// <summary>
  /// Updates a user
  /// </summary>
  /// <param name="user">A <see cref="User"/> instance</param>
  /// <returns>The updated user as a <see cref="User"/> instance</returns>
  Task<User?> UpdateUserAsync(User user);
}