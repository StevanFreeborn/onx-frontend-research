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

record LoginRequest([FromBody] LoginDto Dto);

record RegisterDto(string Email, string Password);

class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
  public RegisterDtoValidator()
  {
    RuleFor(dto => dto.Email).NotEmpty().EmailAddress();
    // At least one uppercase letter - (?=.*[A-Z])
    // At least one lowercase letter - (?=.*[a-z])
    // At least one number - (?=.*\d)
    // At least one special character - (?=.*\W)
    // Must be at least 8 characters long - {8,}
    RuleFor(dto => dto.Password).NotEmpty().Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W)[A-Za-z\d\W]{8,}$");
  }
}

record RegisterRequest([FromBody] RegisterDto Dto);
