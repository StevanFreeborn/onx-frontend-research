using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Server.API.Services;

class JwtOptions
{
  public string Secret { get; init; } = string.Empty;
  public string Issuer { get; init; } = string.Empty;
  public string Audience { get; init; } = string.Empty;
  public int ExpiryInMinutes { get; init; }
}

class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
  private const string SectionName = nameof(JwtOptions);
  private readonly IConfiguration _configuration;

  public JwtOptionsSetup(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public void Configure(JwtOptions options)
  {
    _configuration
      .GetSection(SectionName)
      .Bind(options);
  }
}

interface ITokenService
{
  string GenerateToken(User user);
}

class TokenService : ITokenService
{
  private readonly JwtOptions _jwtOptions;

  public TokenService(IOptions<JwtOptions> jwtOptions)
  {
    _jwtOptions = jwtOptions.Value;
  }

  public string GenerateToken(User user)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
    var issuedAt = DateTime.UtcNow;
    var expires = issuedAt.AddMinutes(_jwtOptions.ExpiryInMinutes);
    var claims = new List<Claim>
    {
      new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new (JwtRegisteredClaimNames.Sub, user.Id),
      new (JwtRegisteredClaimNames.NameId, user.Username),
      new (JwtRegisteredClaimNames.Email, user.Email),
    };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = expires,
      IssuedAt = issuedAt,
      Issuer = _jwtOptions.Issuer,
      Audience = _jwtOptions.Audience,
      SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha256Signature
      )
    };

    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
    var jwtToken = tokenHandler.WriteToken(securityToken);
    return jwtToken;
  }
}