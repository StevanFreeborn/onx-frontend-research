
namespace Server.API.Services;

class UserService
{
  private readonly IUserRepository _userRepository;

  internal UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<Result<string>> RegisterUser(User newUser)
  {
    var existingUser = _userRepository.GetUserByEmailAsync(newUser.Email);

    if (existingUser is not null)
    {
      return Result.Fail(new UserAlreadyExistError(newUser.Email));
    }

    // hash password
    // store user
    // return userId

    return Result.Ok("");
  }
}

class UserAlreadyExistError : Error
{
  internal UserAlreadyExistError(string email) : base($"User already exists with {email}")
  {
  }
}