namespace Server.API.Models;

record LoginDto(string Email, string Password);
record LoginRequest([FromBody] LoginDto Dto);

record RegisterDto(string Email, string Password);
record RegisterRequest([FromBody] RegisterDto Dto);