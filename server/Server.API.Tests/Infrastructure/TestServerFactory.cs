namespace Server.API.Tests.Infrastructure;

class TestServerFactory<TProgram>
: WebApplicationFactory<TProgram> where TProgram : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureTestServices(services =>
    {
    });
  }
}