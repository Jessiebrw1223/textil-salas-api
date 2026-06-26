using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TextilSalas.Application.Abstractions;
using TextilSalas.Infrastructure.Data;
using TextilSalas.Infrastructure.Repositories;
using TextilSalas.Infrastructure.Services;

namespace TextilSalas.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IQuotationRepository, QuotationRepository>();
        services.AddScoped<IAdminAuthService, AdminAuthService>();
        return services;
    }
}
