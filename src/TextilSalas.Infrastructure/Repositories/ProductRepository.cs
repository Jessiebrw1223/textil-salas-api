using Microsoft.EntityFrameworkCore;
using TextilSalas.Application.Abstractions;
using TextilSalas.Application.DTOs;
using TextilSalas.Domain.Entities;
using TextilSalas.Infrastructure.Data;

namespace TextilSalas.Infrastructure.Repositories;

public sealed class ProductRepository(AppDbContext db) : IProductRepository
{
    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken ct) =>
        await db.Categories.AsNoTracking().OrderBy(x => x.SortOrder)
            .Select(x => new CategoryDto(x.Id, x.Name, x.Description, x.Icon, x.IsActive, x.SortOrder)).ToListAsync(ct);

    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(bool includeInactive, CancellationToken ct) =>
        await db.Products.AsNoTracking().Include(x => x.Category).Include(x => x.Images)
            .Where(x => includeInactive || x.IsActive).OrderBy(x => x.Name)
            .Select(x => ToDto(x)).ToListAsync(ct);

    public async Task<ProductDto?> GetProductAsync(Guid id, CancellationToken ct) =>
        await db.Products.AsNoTracking().Include(x => x.Category).Include(x => x.Images).Where(x => x.Id == id)
            .Select(x => ToDto(x)).FirstOrDefaultAsync(ct);

    public async Task<Guid> CreateProductAsync(UpsertProductRequest r, CancellationToken ct)
    {
        var p = new Product { Name = r.Name, CategoryId = r.CategoryId, Price = r.Price, Stock = r.Stock, Material = r.Material, Description = r.Description, ShortDescription = r.ShortDescription, Gramaje = r.Gramaje, Sizes = r.Sizes, IsActive = r.IsActive, IsFeatured = r.IsFeatured };
        db.Products.Add(p); await db.SaveChangesAsync(ct); return p.Id;
    }

    public async Task UpdateProductAsync(Guid id, UpsertProductRequest r, CancellationToken ct)
    {
        var p = await db.Products.FirstOrDefaultAsync(x => x.Id == id, ct) ?? throw new KeyNotFoundException("Producto no encontrado");
        p.Name = r.Name; p.CategoryId = r.CategoryId; p.Price = r.Price; p.Stock = r.Stock; p.Material = r.Material; p.Description = r.Description; p.ShortDescription = r.ShortDescription; p.Gramaje = r.Gramaje; p.Sizes = r.Sizes; p.IsActive = r.IsActive; p.IsFeatured = r.IsFeatured; p.UpdatedAt = DateTimeOffset.UtcNow;
        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken ct)
    {
        var p = await db.Products.FirstOrDefaultAsync(x => x.Id == id, ct) ?? throw new KeyNotFoundException("Producto no encontrado");
        db.Products.Remove(p); await db.SaveChangesAsync(ct);
    }

    private static ProductDto ToDto(Product x) => new(x.Id, x.Name, x.CategoryId, x.Category?.Name, x.Price, x.Stock, x.Material, x.Description, x.ShortDescription, x.Gramaje, x.Sizes, x.IsActive, x.IsFeatured, x.Images.OrderBy(i => i.SortOrder).Select(i => new ProductImageDto(i.Id, i.ImageUrl, i.SortOrder)).ToList());
}
