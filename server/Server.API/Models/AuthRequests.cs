namespace Server.API.Models;

record LoginDto(string Email, string Password);
record LoginRequest([FromBody] LoginDto Dto);