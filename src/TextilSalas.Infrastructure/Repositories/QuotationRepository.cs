using Microsoft.EntityFrameworkCore;
using TextilSalas.Application.Abstractions;
using TextilSalas.Application.DTOs;
using TextilSalas.Domain.Entities;
using TextilSalas.Infrastructure.Data;

namespace TextilSalas.Infrastructure.Repositories;
public sealed class QuotationRepository(AppDbContext db) : IQuotationRepository
{
    public async Task<QuotationDto> CreateAsync(CreateQuotationRequest r, CancellationToken ct)
    {
        var q = new Quotation { ContactName = r.ContactName, LastName = r.LastName, Company = r.Company, Ruc = r.Ruc, Email = r.Email, Phone = r.Phone, BusinessType = r.BusinessType, ProductInterest = r.ProductInterest, Quantity = r.Quantity, Details = r.Details };
        db.Quotations.Add(q); await db.SaveChangesAsync(ct); return ToDto(q);
    }
    public async Task<IReadOnlyList<QuotationDto>> GetAllAsync(CancellationToken ct) => await db.Quotations.AsNoTracking().OrderByDescending(x => x.CreatedAt).Select(x => ToDto(x)).ToListAsync(ct);
    private static QuotationDto ToDto(Quotation q) => new(q.Id, q.ContactName, q.Company, q.Email, q.Phone, q.BusinessType, q.Status, q.Priority, q.EstimatedAmount, q.CreatedAt);
}
