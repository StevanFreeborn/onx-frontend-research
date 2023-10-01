namespace Server.API.Models;

/// <summary>
/// Represents the data needed to login
/// </summary>
record LoginDto(string Username, string Password);

/// <summary>
/// Validator for <see cref="LoginDto"/>
/// </summary>
class LoginDtoValidator : AbstractValidator<LoginDto>
{
  public LoginDtoValidator()
  {
    RuleFor(dto => dto.Username).NotEmpty();
    RuleFor(dto => dto.Password).NotEmpty();
  }
}

/// <summary>
/// Represents a login request
/// </summary>
record LoginRequest(
  HttpContext Context,
  [FromBody] LoginDto Dto,
  [FromServices] IValidator<LoginDto> Validator,
  [FromServices] IUserService UserService
);

/// <summary>
/// Represents the data needed to register
/// </summary>
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

/// <summary>
/// Validator for <see cref="RegisterDto"/>
/// </summary>
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

/// <summary>
/// Represents a register request
/// </summary>
record RegisterRequest(
  [FromBody] RegisterDto Dto,
  [FromServices] IValidator<RegisterDto> Validator,
  [FromServices] IUserService UserService
);

/// <summary>
/// Represents logout request
/// </summary>
record LogoutRequest(
  HttpContext Context,
  [FromServices] IUserService UserService
);

/// <summary>
/// Represents the data needed to refresh a token
/// </summary>
record RefreshTokenDto(string UserId);

/// <summary>
/// Validator for <see cref="RefreshTokenDto"/>
/// </summary>
class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
{
  public RefreshTokenDtoValidator()
  {
    RuleFor(dto => dto.UserId).NotEmpty().WithMessage("UserId is required.");
  }
}

/// <summary>
/// Represents a refresh token request
/// </summary>
record RefreshTokenRequest(
  HttpContext Context,
  [FromBody] RefreshTokenDto Dto,
  [FromServices] IValidator<RefreshTokenDto> Validator,
  [FromServices] IUserService UserService
);