using System.Text.Json;

namespace TextilSalas.Domain.Entities;

public sealed class Order : BaseEntity
{
    public Guid? UserId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = "pendiente";
    public string PaymentStatus { get; set; } = "pendiente";
    public string DeliveryType { get; set; } = string.Empty;
    public string? Department { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? FullAddress { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string? PaymentReference { get; set; }
    public JsonDocument Items { get; set; } = JsonDocument.Parse("[]");
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Igv { get; set; }
    public decimal Total { get; set; }
    public bool WantsInvoice { get; set; }
    public string? Ruc { get; set; }
    public string? RazonSocial { get; set; }
    public string? BoletaNumber { get; set; }
    public string? PaymentProofUrl { get; set; }
    public string? PaymentOperationNumber { get; set; }
    public DateTimeOffset? PaymentSubmittedAt { get; set; }
    public string? PaymentRejectionReason { get; set; }
    public DateTimeOffset? PaymentValidatedAt { get; set; }
    public Guid? PaymentValidatedBy { get; set; }
}
