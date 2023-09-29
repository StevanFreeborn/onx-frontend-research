namespace Server.API.Services;

class UserService
{
  private readonly IUserRepository _userRepository;

  internal UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }
}