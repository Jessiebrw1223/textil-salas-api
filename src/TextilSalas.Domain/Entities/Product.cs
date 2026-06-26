namespace TextilSalas.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? Material { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? Gramaje { get; set; }
    public string[] Sizes { get; set; } = Array.Empty<string>();
    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; }
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}
