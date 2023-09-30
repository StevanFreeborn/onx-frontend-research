namespace Server.API.Models;

class User
{
  public string Id { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Username => Email;
  public List<string> PreviousPasswords = new();
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public List<RefreshToken> RefreshTokens { get; set; } = new();
}