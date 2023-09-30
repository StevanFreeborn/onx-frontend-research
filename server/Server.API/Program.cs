var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureOptions<MongoDbOptionsSetup>();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IUserRepository, MongoUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();

var app = builder.Build();

app.UseMiddleware<ErrorMiddleware>();

app.MapAuthEndpoints();

app.Run();