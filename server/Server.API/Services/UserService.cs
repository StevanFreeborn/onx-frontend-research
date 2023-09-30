
namespace Server.API.Services;


interface IUserService
{
  Task<Result<string>> RegisterUserAsync(User newUser);
  Task<Result<User>> GetUserAsync(string userId);
  Task<Result<(string AccessToken, string RefreshToken)>> LoginUserAsync(string username, string password);
}

class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly ITokenService _tokenService;

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

  public async Task<Result<(string AccessToken, string RefreshToken)>> LoginUserAsync(string username, string password)
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

    return Result.Ok((accessToken, refreshToken.Token));
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