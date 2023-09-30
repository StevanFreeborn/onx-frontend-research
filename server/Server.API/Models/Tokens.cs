namespace Server.API.Models;

enum TokenType
{
  Refresh = 0,
}

record BaseToken
{
  public string Id { get; init; } = string.Empty;
  public string Token { get; init; } = string.Empty;
  public DateTime ExpiresAt { get; init; } = DateTime.UtcNow;
  public bool Revoked { get; init; } = false;
  public TokenType TokenType { get; init; }
  public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
}

record RefreshToken : BaseToken
{
  internal RefreshToken() : base()
  {
    TokenType = TokenType.Refresh;
  }
}