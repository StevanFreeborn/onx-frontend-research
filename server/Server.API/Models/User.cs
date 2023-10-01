namespace Server.API.Models;

/// <summary>
/// Represents a user
/// </summary>
class User
{
  /// <summary>
  /// The user's id
  /// </summary>
  public string Id { get; set; } = string.Empty;

  /// <summary>
  /// The user's email
  /// </summary>
  public string Email { get; set; } = string.Empty;

  /// <summary>
  /// The user's hashed password
  /// </summary>
  public string Password { get; set; } = string.Empty;

  /// <summary>
  /// The user's username
  /// </summary>
  public string Username => Email;

  /// <summary>
  /// The user's previous hashed passwords
  /// </summary>
  public List<string> PreviousPasswords = new();

  /// <summary>
  /// The user's created date
  /// </summary>
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  /// <summary>
  /// The user's updated date
  /// </summary>
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

  /// <summary>
  /// The user's refresh tokens
  /// </summary>
  public List<RefreshToken> RefreshTokens { get; set; } = new();
}