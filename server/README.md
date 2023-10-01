# Server

## Description

This is a server that acts as a backend for the Onx clients.

## Setup

1. Restore dependencies: `dotnet restore`
1. Set values for `MongoDbOptions` in `appsettings.json` or `appsettings.Development.json`
1. Set values for `JwtOptions` in `appsettings.json` or `appsettings.Development.json`
1. Run the server: `dotnet run --project ./Server.API/`

## Testing

1. Run the test suite: `dotnet test`

## API Dependencies

- [MongoDB.Driver](https://mongodb.github.io/mongo-csharp-driver/)
- [FluentValidation](https://fluentvalidation.net/)
- [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net)
- [FluentResults](https://github.com/altmann/FluentResults)
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)

## Testing Dependencies
