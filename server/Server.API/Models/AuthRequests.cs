namespace Server.API.Models;

record LoginDto(string Username, string Password);

class LoginDtoValidator : AbstractValidator<LoginDto>
{
  public LoginDtoValidator()
  {
    RuleFor(dto => dto.Username).NotEmpty();
    RuleFor(dto => dto.Password).NotEmpty();
  }
}

record LoginRequest(
  HttpContext Context,
  [FromBody] LoginDto Dto,
  [FromServices] IValidator<LoginDto> Validator,
  [FromServices] IUserService UserService
);

record RegisterDto(string Email, string Password)
{
  internal User ToUser()
  {
    return new User
    {
      Email = Email,
      Password = Password
    };
  }
}

class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
  public RegisterDtoValidator()
  {
    RuleFor(dto => dto.Email)
      .NotEmpty()
      .EmailAddress()
      .WithMessage("Email must be a valid email address.");
    // At least one uppercase letter - (?=.*[A-Z])
    // At least one lowercase letter - (?=.*[a-z])
    // At least one number - (?=.*\d)
    // At least one special character - (?=.*\W)
    // Must be at least 8 characters long - {8,}
    RuleFor(dto => dto.Password)
      .NotEmpty()
      .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W)[A-Za-z\d\W]{8,}$")
      .WithMessage("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character.");
  }
}

record RegisterRequest(
  [FromBody] RegisterDto Dto,
  [FromServices] IValidator<RegisterDto> Validator,
  [FromServices] IUserService UserService
);

record LogoutRequest(
  HttpContext Context
);