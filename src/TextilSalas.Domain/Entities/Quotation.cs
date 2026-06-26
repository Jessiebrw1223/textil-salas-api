namespace TextilSalas.Domain.Entities;

public sealed class Quotation : BaseEntity
{
    public string ContactName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string Company { get; set; } = string.Empty;
    public string? Ruc { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string? ProductInterest { get; set; }
    public string? Quantity { get; set; }
    public string Details { get; set; } = string.Empty;
    public string Status { get; set; } = "pendiente";
    public string Priority { get; set; } = "media";
    public decimal EstimatedAmount { get; set; }
    public string? Location { get; set; }
    public DateTimeOffset? LastContactDate { get; set; }
    public DateTimeOffset? FollowUpDate { get; set; }
    public string? InternalNotes { get; set; }
}
