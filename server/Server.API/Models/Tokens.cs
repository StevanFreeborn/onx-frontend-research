namespace Server.API.Models;

enum TokenType
{
  Refresh = 0,
}

record BaseToken
{
  public string Id { get; init; } = string.Empty;
  public string Token { get; init; } = string.Empty;
  public string UserId { get; init; } = string.Empty;
  public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow;
  public bool Revoked { get; init; } = false;
  public TokenType TokenType { get; init; }
  public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
  public DateTimeOffset UpdatedAt { get; init; } = DateTimeOffset.UtcNow;
}

record RefreshToken : BaseToken
{
  internal RefreshToken() : base()
  {
    TokenType = TokenType.Refresh;
  }
}