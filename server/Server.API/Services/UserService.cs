
namespace Server.API.Services;

/// <summary>
/// Represents a service for handling user data
/// </summary>
interface IUserService
{
  /// <summary>
  /// Registers a new user
  /// </summary>
  /// <param name="newUser">A <see cref="User"/> instance</param>
  /// <returns>The new user's id</returns>
  Task<Result<string>> RegisterUserAsync(User newUser);

  /// <summary>
  /// Gets a user by id
  /// </summary>
  /// <param name="userId">The user's id</param>
  /// <returns>The user as a <see cref="User"/> instance</returns>
  Task<Result<User>> GetUserAsync(string userId);

  /// <summary>
  /// Logs in a user
  /// </summary>
  /// <param name="username">The user's username</param>
  /// <param name="password">The user's password</param>
  /// <returns>A tuple containing the access token and refresh token as a <see cref="string"/> and <see cref="RefreshToken"/> respectively</returns>
  Task<Result<(string AccessToken, RefreshToken RefreshToken)>> LoginUserAsync(string username, string password);

  /// <summary>
  /// Revokes a refresh token
  /// </summary>
  /// <param name="userId">The user's id</param>
  /// <param name="token">The refresh token</param>
  /// <returns>A <see cref="Task"/></returns>
  Task RevokeRefreshTokenAsync(string userId, string token);

  /// <summary>
  /// Removes all invalid refresh tokens
  /// </summary>
  /// <param name="userId">The user's id whose invalid refresh tokens should be removed</param>
  /// <returns>A <see cref="Task"/></returns>
  Task RemoveAllInvalidRefreshTokensAsync(string userId);

  /// <summary>
  /// Refreshes a user's access token
  /// </summary>
  /// <param name="userId">The user's id</param>
  /// <param name="token">The refresh token</param>
  /// <returns>A tuple containing the access token and refresh token as a <see cref="string"/> and <see cref="RefreshToken"/> respectively</returns>
  Task<Result<(string AccessToken, RefreshToken RefreshToken)>> RefreshTokenAsync(string userId, string token);
}

/// <summary>
/// Represents a service for handling user data
/// </summary>
/// <inheritdoc/>
class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly ITokenService _tokenService;

  /// <summary>
  /// Creates a new <see cref="UserService"/> instance
  /// </summary>
  /// <param name="userRepository">A <see cref="IUserRepository"/> instance</param>
  /// <param name="tokenService">A <see cref="ITokenService"/> instance</param>
  /// <returns>A <see cref="UserService"/> instance</returns>
  public UserService(IUserRepository userRepository, ITokenService tokenService)
  {
    _userRepository = userRepository;
    _tokenService = tokenService;
  }

  public async Task<Result<string>> RegisterUserAsync(User newUser)
  {
    var existingUser = await _userRepository.GetUserByEmailAsync(newUser.Email);

    if (existingUser is not null)
    {
      return Result.Fail(new UserAlreadyExistError(newUser.Email));
    }

    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
    newUser.Password = hashedPassword;

    var createdUser = await _userRepository.CreateUserAsync(newUser);

    return Result.Ok(createdUser.Id);
  }

  public async Task<Result<User>> GetUserAsync(string userId)
  {
    var existingUser = await _userRepository.GetUserByIdAsync(userId);

    if (existingUser is null)
    {
      return Result.Fail(new UserNotFoundError(userId));
    }

    return Result.Ok(existingUser);
  }

  public async Task<Result<(string AccessToken, RefreshToken RefreshToken)>> LoginUserAsync(string username, string password)
  {
    var existingUser = await _userRepository.GetUserByUsernameAsync(username);

    if (existingUser is null)
    {
      return Result.Fail(new InvalidLoginError());
    }

    var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, existingUser.Password);

    if (isPasswordValid is false)
    {
      return Result.Fail(new InvalidLoginError());
    }

    var accessToken = _tokenService.GenerateJwtToken(existingUser);
    var refreshToken = _tokenService.GenerateRefreshToken();

    existingUser.RefreshTokens.Add(refreshToken);

    var updatedUser = await _userRepository.UpdateUserAsync(existingUser);

    if (updatedUser is null)
    {
      return Result.Fail(new InvalidLoginError());
    }

    return Result.Ok((accessToken, refreshToken));
  }

  public async Task RevokeRefreshTokenAsync(string userId, string token)
  {
    var existingUser = await _userRepository.GetUserByIdAsync(userId);

    if (existingUser is null)
    {
      return;
    }

    var refreshToken = existingUser.RefreshTokens.SingleOrDefault(x => x.Token == token);

    if (refreshToken is null)
    {
      return;
    }

    var updatedRefreshToken = refreshToken with { Revoked = true, UpdatedAt = DateTime.UtcNow };

    existingUser.RefreshTokens.Remove(refreshToken);
    existingUser.RefreshTokens.Add(updatedRefreshToken);

    await _userRepository.UpdateUserAsync(existingUser);
  }

  public async Task RemoveAllInvalidRefreshTokensAsync(string userId)
  {
    var existingUser = await _userRepository.GetUserByIdAsync(userId);

    if (existingUser is null)
    {
      return;
    }

    var invalidRefreshTokens = existingUser.RefreshTokens
      .Where(x => x.Revoked || x.ExpiresAt < DateTime.UtcNow)
      .ToList();

    existingUser.RefreshTokens.RemoveAll(invalidRefreshTokens.Contains);

    await _userRepository.UpdateUserAsync(existingUser);
  }

  public async Task<Result<(string AccessToken, RefreshToken RefreshToken)>> RefreshTokenAsync(string userId, string token)
  {
    var existingUser = await _userRepository.GetUserByIdAsync(userId);

    if (existingUser is null)
    {
      return Result.Fail(new UserNotFoundError(userId));
    }

    var refreshToken = existingUser.RefreshTokens.SingleOrDefault(x => x.Token == token);

    if (refreshToken is null)
    {
      return Result.Fail(new InvalidRefreshToken());
    }

    if (refreshToken.Revoked || refreshToken.ExpiresAt < DateTime.UtcNow)
    {
      return Result.Fail(new InvalidRefreshToken());
    }

    var newRefreshToken = _tokenService.GenerateRefreshToken();
    var newAccessToken = _tokenService.GenerateJwtToken(existingUser);

    existingUser.RefreshTokens.Add(newRefreshToken);

    var updatedUser = await _userRepository.UpdateUserAsync(existingUser);

    if (updatedUser is null)
    {
      return Result.Fail(new RefreshTokenError());
    }

    return Result.Ok((newAccessToken, newRefreshToken));
  }
}

class RefreshTokenError : Error
{
  internal RefreshTokenError() : base("Unable to refresh token")
  {
  }
}

class InvalidRefreshToken : Error
{
  internal InvalidRefreshToken() : base("Refresh token is invalid")
  {
  }
}

class InvalidLoginError : Error
{
  internal InvalidLoginError() : base("Username/Password combination is not valid")
  {
  }
}

class UserAlreadyExistError : Error
{
  internal UserAlreadyExistError(string email) : base($"User already exists with email: {email}")
  {
  }
}

class UserNotFoundError : Error
{
  internal UserNotFoundError(string id) : base($"User not found with identifier: {id}")
  {
  }
}