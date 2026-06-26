using TextilSalas.Application.DTOs;
namespace TextilSalas.Application.Abstractions;
public interface IQuotationRepository
{
    Task<QuotationDto> CreateAsync(CreateQuotationRequest request, CancellationToken ct);
    Task<IReadOnlyList<QuotationDto>> GetAllAsync(CancellationToken ct);
}
