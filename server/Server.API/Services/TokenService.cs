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
  string GenerateJwtToken(User user);
  RefreshToken GenerateRefreshToken();
}

class TokenService : ITokenService
{
  private readonly JwtOptions _jwtOptions;

  public TokenService(IOptions<JwtOptions> jwtOptions)
  {
    _jwtOptions = jwtOptions.Value;
  }

  public string GenerateJwtToken(User user)
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

  public RefreshToken GenerateRefreshToken()
  {
    var token = new RefreshToken
    {
      Id = Guid.NewGuid().ToString(),
      Token = GenerateToken(),
      ExpiresAt = DateTime.UtcNow.AddHours(12),
    };

    return token;
  }

  private string GenerateToken()
  {
    var randomBytes = new byte[32];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(randomBytes);
    var token = Convert.ToBase64String(randomBytes);
    return RemoveNonAlphaNumericCharacters(token);
  }

  private string RemoveNonAlphaNumericCharacters(string input)
  {
    var pattern = @"[^A-Za-z0-9]";
    var replacement = string.Empty;
    var output = Regex.Replace(input, pattern, replacement);
    return output;
  }
}