var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureOptions<MongoDbOptionsSetup>();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IUserRepository, MongoUserRepository>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.MapAuthEndpoints();

app.Run();
