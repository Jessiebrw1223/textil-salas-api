namespace TextilSalas.Domain.Entities;

public sealed class Profile : BaseEntity
{
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MaternalLastName { get; set; }
    public string? Dni { get; set; }
    public string? Gender { get; set; }
    public string? Phone { get; set; }
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
