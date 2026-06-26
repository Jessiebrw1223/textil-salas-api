using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TextilSalas.Application.Abstractions;
using TextilSalas.Application.DTOs;

namespace TextilSalas.Infrastructure.Services;

public sealed class AdminAuthService(IConfiguration config) : IAdminAuthService
{
    public Task<AdminLoginResponse> LoginAsync(AdminLoginRequest request, CancellationToken ct)
    {
        var email = config["Admin:Email"];
        var password = config["Admin:Password"];
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) throw new InvalidOperationException("Admin no configurado en variables de entorno.");
        if (!string.Equals(request.Email, email, StringComparison.OrdinalIgnoreCase) || request.Password != password) throw new UnauthorizedAccessException("Credenciales inválidas.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key no configurado")));
        var token = new JwtSecurityToken(issuer: config["Jwt:Issuer"], audience: config["Jwt:Audience"], claims: [new Claim(ClaimTypes.Email, request.Email), new Claim(ClaimTypes.Role, "admin")], expires: DateTime.UtcNow.AddHours(8), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        return Task.FromResult(new AdminLoginResponse(new JwtSecurityTokenHandler().WriteToken(token), request.Email, "admin"));
    }
}
