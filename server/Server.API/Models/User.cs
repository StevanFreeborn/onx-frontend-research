namespace Server.API.Models;

class User
{
  public string Id { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Username => Email;
  public string Salt { get; set; } = string.Empty;
  public List<string> PreviousPasswords = new();
  public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
  public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
  public List<RefreshToken> RefreshTokens { get; set; } = new();
}