namespace Server.API.Models;

/// <summary>
/// Represents the options used to configure CORS
/// </summary>
class CorsOptions
{
  /// <summary>
  /// The allowed origins
  /// </summary>
  public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
}