namespace Server.API.Middleware;

/// <summary>
/// Middleware for handling errors
/// </summary>
class ErrorMiddleware
{
  private readonly RequestDelegate _next;

  /// <summary>
  /// Creates a new <see cref="ErrorMiddleware"/> instance
  /// </summary>
  /// <param name="next">The next middleware in the pipeline</param>
  /// <returns>A <see cref="ErrorMiddleware"/> instance</returns>
  public ErrorMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  /// <summary>
  /// Invokes the middleware
  /// </summary>
  /// <param name="context">The <see cref="HttpContext"/> instance</param>
  /// <returns>A <see cref="Task"/></returns>
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      var problem = new ProblemDetails
      {
        Status = (int)HttpStatusCode.InternalServerError,
        Title = "An problem occurred while processing the request",
        Detail = ex.Message,
      };

      context.Response.StatusCode = problem.Status.Value;
      context.Response.ContentType = "application/problem+json";
      await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
  }
}