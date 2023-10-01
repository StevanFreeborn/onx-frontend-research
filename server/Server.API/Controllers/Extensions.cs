namespace Server.API.Controllers;

static class HttpContextExtensions
{
  internal static string? GetUserId(this HttpContext context)
  {
    return context.User.FindFirstValue(ClaimTypes.NameIdentifier);
  }
}

static class HttpResponseExtensions
{
  internal static void SetRefreshTokenCookie(this HttpResponse response, string token, DateTime expiresAt)
  {
    response.Cookies.Append(
      "onxRefreshToken",
      token,
      new CookieOptions
      {
        HttpOnly = true,
        Expires = expiresAt,
        SameSite = SameSiteMode.None,
        Secure = true
      }
    );
  }
}

static class HttpRequestExtensions
{
  internal static string? GetRefreshTokenCookie(this HttpRequest request)
  {
    return request.Cookies["onxRefreshToken"];
  }
}