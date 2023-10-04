namespace Server.API.Services;

/// <summary>
/// Represents options used to configure JWT authentication
/// </summary>
class JwtOptions
{
  /// <summary>
  /// The secret used to sign the JWT
  /// </summary>
  public string Secret { get; set; } = string.Empty;

  /// <summary>
  /// The issuer of the JWT
  /// </summary>
  public string Issuer { get; set; } = string.Empty;

  /// <summary>
  /// The audience of the JWT
  /// </summary>
  public string Audience { get; set; } = string.Empty;

  /// <summary>
  /// The expiry of the JWT in minutes
  /// </summary>
  public int ExpiryInMinutes { get; set; }
}

/// <summary>
/// Configures <see cref="JwtOptions"/>
/// </summary>
class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
  private const string SectionName = nameof(JwtOptions);
  private readonly IConfiguration _configuration;

  /// <summary>
  /// Creates a new <see cref="JwtOptionsSetup"/> instance
  /// </summary>
  /// <param name="configuration">A <see cref="IConfiguration"/> instance</param>
  /// <returns>A <see cref="JwtOptionsSetup"/> instance</returns>
  public JwtOptionsSetup(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  /// <summary>
  /// Configures <see cref="JwtOptions"/>
  /// </summary>
  /// <param name="options">A <see cref="JwtOptions"/> instance</param>
  /// <returns>A <see cref="JwtOptions"/> instance</returns>
  public void Configure(JwtOptions options)
  {
    _configuration
      .GetSection(SectionName)
      .Bind(options);
  }
}

/// <summary>
/// Represents a service for handling authentication tokens
/// </summary>
interface ITokenService
{
  /// <summary>
  /// Generates a JWT token
  /// </summary>
  /// <param name="user">A <see cref="User"/> instance</param>
  /// <returns>A JWT token</returns>
  string GenerateJwtToken(User user);

  /// <summary>
  /// Generates a refresh token
  /// </summary>
  /// <returns>A <see cref="RefreshToken"/> instance</returns>
  RefreshToken GenerateRefreshToken();
}

/// <summary>
/// Represents a service for handling authentication tokens
/// </summary>
/// <inheritdoc/>
class TokenService : ITokenService
{
  private readonly JwtOptions _jwtOptions;

  /// <summary>
  /// Creates a new <see cref="TokenService"/> instance
  /// </summary>
  /// <param name="jwtOptions">A <see cref="IOptions{JwtOptions}"/> instance</param>
  /// <returns>A <see cref="TokenService"/> instance</returns>
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