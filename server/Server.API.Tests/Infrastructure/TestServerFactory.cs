namespace Server.API.Tests.Infrastructure;

public class TestServerFactory<TProgram>
: WebApplicationFactory<TProgram> where TProgram : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureAppConfiguration((context, config) =>
    {
      var projectDir = Directory.GetCurrentDirectory();
      var testConfig = Path.Combine(projectDir, "integrationtestsettings.json");
      config.AddJsonFile(testConfig);
    });

    builder.ConfigureTestServices(services =>
    {
      services.Configure<JwtOptions>(options =>
      {
        options.Secret = "Test";
        options.Audience = "Test";
        options.Issuer = "Test";
        options.ExpiryInMinutes = 5;
      });
      services.Configure<MongoDbOptions>(options =>
      {
        var runner = MongoDbRunner.Start();
        options.ConnectionString = runner.ConnectionString;
        options.DatabaseName = "IntegrationTests";
      });
    });
  }
}