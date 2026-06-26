namespace TextilSalas.Application.DTOs;
public sealed record AdminLoginRequest(string Email, string Password);
public sealed record AdminLoginResponse(string AccessToken, string Email, string Role);
