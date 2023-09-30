namespace Server.API.Middleware;

class ErrorMiddleware
{
  private readonly RequestDelegate _next;

  public ErrorMiddleware(RequestDelegate next)
  {
    _next = next;
  }

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