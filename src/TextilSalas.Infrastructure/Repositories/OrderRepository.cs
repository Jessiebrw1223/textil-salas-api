using Microsoft.EntityFrameworkCore;
using TextilSalas.Application.Abstractions;
using TextilSalas.Application.DTOs;
using TextilSalas.Application.Services;
using TextilSalas.Domain.Entities;
using TextilSalas.Infrastructure.Data;

namespace TextilSalas.Infrastructure.Repositories;

public sealed class OrderRepository(AppDbContext db) : IOrderRepository
{
    public async Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken ct) =>
        await db.Orders.AsNoTracking().OrderByDescending(x => x.CreatedAt).Select(x => ToDto(x)).ToListAsync(ct);

    public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest r, CancellationToken ct)
    {
        var subtotal = r.Subtotal;
        var igv = Math.Round(subtotal * 0.18m, 2);
        var total = subtotal - r.Discount + r.ShippingCost + igv;
        var order = new Order { OrderNumber = OrderNumberGenerator.NewOrderNumber(), Email = r.Email, UserId = r.UserId, DeliveryType = r.DeliveryType, Department = r.Department, Province = r.Province, District = r.District, FullAddress = r.FullAddress, PaymentMethod = r.PaymentMethod, Items = r.Items, Subtotal = subtotal, Discount = r.Discount, ShippingCost = r.ShippingCost, Igv = igv, Total = total, WantsInvoice = r.WantsInvoice, Ruc = r.Ruc, RazonSocial = r.RazonSocial };
        db.Orders.Add(order); await db.SaveChangesAsync(ct); return ToDto(order);
    }

    public async Task UpdateStatusAsync(Guid id, string status, CancellationToken ct)
    {
        var order = await db.Orders.FirstOrDefaultAsync(x => x.Id == id, ct) ?? throw new KeyNotFoundException("Pedido no encontrado");
        order.Status = status; await db.SaveChangesAsync(ct);
    }

    public async Task SubmitPaymentProofAsync(Guid id, SubmitPaymentProofRequest r, CancellationToken ct)
    {
        var order = await db.Orders.FirstOrDefaultAsync(x => x.Id == id, ct) ?? throw new KeyNotFoundException("Pedido no encontrado");
        order.PaymentOperationNumber = r.PaymentOperationNumber; order.PaymentProofUrl = r.PaymentProofUrl; order.PaymentSubmittedAt = DateTimeOffset.UtcNow; order.PaymentStatus = "en_revision";
        await db.SaveChangesAsync(ct);
    }

    private static OrderDto ToDto(Order x) => new(x.Id, x.OrderNumber, x.Email, x.Status, x.PaymentStatus, x.PaymentMethod, x.Total, x.CreatedAt);
}
