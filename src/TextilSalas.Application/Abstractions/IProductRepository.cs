using TextilSalas.Application.DTOs;
namespace TextilSalas.Application.Abstractions;
public interface IProductRepository
{
    Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
    Task<IReadOnlyList<ProductDto>> GetProductsAsync(bool includeInactive, CancellationToken ct);
    Task<ProductDto?> GetProductAsync(Guid id, CancellationToken ct);
    Task<Guid> CreateProductAsync(UpsertProductRequest request, CancellationToken ct);
    Task UpdateProductAsync(Guid id, UpsertProductRequest request, CancellationToken ct);
    Task DeleteProductAsync(Guid id, CancellationToken ct);
}
