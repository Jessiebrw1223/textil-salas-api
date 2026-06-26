using System.Text.Json;
namespace TextilSalas.Application.DTOs;
public sealed record CreateOrderRequest(string Email, Guid? UserId, string DeliveryType, string? Department, string? Province, string? District, string? FullAddress, string PaymentMethod, JsonDocument Items, decimal Subtotal, decimal Discount, decimal ShippingCost, bool WantsInvoice, string? Ruc, string? RazonSocial);
public sealed record OrderDto(Guid Id, string OrderNumber, string Email, string Status, string PaymentStatus, string PaymentMethod, decimal Total, DateTimeOffset CreatedAt);
public sealed record UpdateOrderStatusRequest(string Status);
public sealed record SubmitPaymentProofRequest(string PaymentOperationNumber, string PaymentProofUrl);
