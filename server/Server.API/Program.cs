using Server.API.Routes;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapAuthEndpoints();

app.Run();
