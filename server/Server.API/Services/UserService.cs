
namespace Server.API.Services;


interface IUserService
{
  Task<Result<string>> RegisterUser(User newUser);
  Task<Result<User>> GetUser(string userId);
}

class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<Result<string>> RegisterUser(User newUser)
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

  public async Task<Result<User>> GetUser(string userId)
  {
    var existingUser = await _userRepository.GetUserByIdAsync(userId);

    if (existingUser is null)
    {
      return Result.Fail(new UserNotFoundError(userId));
    }

    return Result.Ok(existingUser);
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
  internal UserNotFoundError(string id) : base($"User not found with id: {id}")
  {
  }
}