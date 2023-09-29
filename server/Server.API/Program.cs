var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureOptions<MongoDbOptionsSetup>();

var app = builder.Build();

app.MapAuthEndpoints();

app.Run();
