var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<MongoDbOptionsSetup>();

var jwtOptions = new JwtOptions();
config.GetSection(nameof(JwtOptions)).Bind(jwtOptions);

var corsOptions = new CorsOptions();
config.GetSection(nameof(CorsOptions)).Bind(corsOptions);

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(
    o => o.TokenValidationParameters = new TokenValidationParameters
    {
      ValidIssuer = jwtOptions.Issuer,
      ValidAudience = jwtOptions.Audience,
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtOptions.Secret)
      ),
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ClockSkew = TimeSpan.FromSeconds(0),
    }
  );

builder.Services.AddCors(
  options => options.AddPolicy(
    "CORSpolicy",
    policy => policy
      .AllowCredentials()
      .AllowAnyHeader()
      .AllowAnyMethod()
      .WithOrigins(corsOptions.AllowedOrigins)
  )
);


builder.Services.AddAuthorization();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IUserRepository, MongoUserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddTransient<IValidator<RefreshTokenDto>, RefreshTokenDtoValidator>();


var app = builder.Build();

app.UseMiddleware<ErrorMiddleware>();

app.MapAuthEndpoints();
app.MapUsersEndpoints();

app.UseCors("CORSpolicy");

app.UseAuthentication();
app.UseAuthorization();

app.Run();