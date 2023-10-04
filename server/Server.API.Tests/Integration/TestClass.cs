namespace Server.API.Tests.Integration;

public class IntegrationTestClass : IClassFixture<TestServerFactory<Program>>
{
  private readonly HttpClient _client;

  public IntegrationTestClass(TestServerFactory<Program> server)
  {
    _client = server.CreateClient();
  }

  [Fact]
  public async Task TestMethod()
  {
    // register a new user
    var registerUser = new
    {
      email = "test@test.com",
      password = "@Password1",
    };

    var registerResponse = await _client.PostAsJsonAsync("/auth/register", registerUser);
    registerResponse.StatusCode.Should().Be(HttpStatusCode.Created);
  }
}