namespace TextilSalas.Application.DTOs;
public sealed record CategoryDto(Guid Id, string Name, string? Description, string Icon, bool IsActive, int SortOrder);
public sealed record ProductImageDto(Guid Id, string ImageUrl, int SortOrder);
public sealed record ProductDto(Guid Id, string Name, Guid? CategoryId, string? CategoryName, decimal Price, int Stock, string? Material, string? Description, string? ShortDescription, string? Gramaje, string[] Sizes, bool IsActive, bool IsFeatured, IReadOnlyList<ProductImageDto> Images);
public sealed record UpsertProductRequest(string Name, Guid? CategoryId, decimal Price, int Stock, string? Material, string? Description, string? ShortDescription, string? Gramaje, string[] Sizes, bool IsActive, bool IsFeatured);
