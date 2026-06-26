using Microsoft.AspNetCore.Mvc;
using TextilSalas.Application.Abstractions;
using TextilSalas.Application.DTOs;

namespace TextilSalas.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IAdminAuthService auth) : ControllerBase
{
    [HttpPost("admin/login")]
    public async Task<ActionResult<AdminLoginResponse>> AdminLogin(AdminLoginRequest request, CancellationToken ct)
    {
        try { return Ok(await auth.LoginAsync(request, ct)); }
        catch (UnauthorizedAccessException) { return Unauthorized(new { message = "Credenciales inválidas" }); }
    }
}
