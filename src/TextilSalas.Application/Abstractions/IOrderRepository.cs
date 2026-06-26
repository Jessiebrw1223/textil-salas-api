using TextilSalas.Application.DTOs;
namespace TextilSalas.Application.Abstractions;
public interface IOrderRepository
{
    Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken ct);
    Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, CancellationToken ct);
    Task UpdateStatusAsync(Guid id, string status, CancellationToken ct);
    Task SubmitPaymentProofAsync(Guid id, SubmitPaymentProofRequest request, CancellationToken ct);
}
