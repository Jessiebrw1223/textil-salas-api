namespace TextilSalas.Application.DTOs;
public sealed record CreateQuotationRequest(string ContactName, string? LastName, string Company, string? Ruc, string Email, string Phone, string BusinessType, string? ProductInterest, string? Quantity, string Details);
public sealed record QuotationDto(Guid Id, string ContactName, string Company, string Email, string Phone, string BusinessType, string Status, string Priority, decimal EstimatedAmount, DateTimeOffset CreatedAt);
