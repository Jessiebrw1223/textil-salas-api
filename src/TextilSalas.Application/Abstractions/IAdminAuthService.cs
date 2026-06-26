using TextilSalas.Application.DTOs;
namespace TextilSalas.Application.Abstractions;
public interface IAdminAuthService { Task<AdminLoginResponse> LoginAsync(AdminLoginRequest request, CancellationToken ct); }
