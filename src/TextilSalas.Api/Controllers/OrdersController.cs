using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextilSalas.Application.Abstractions;
using TextilSalas.Application.DTOs;

namespace TextilSalas.Api.Controllers;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController(IOrderRepository orders) : ControllerBase
{
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> Get(CancellationToken ct) => Ok(await orders.GetOrdersAsync(ct));

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderRequest request, CancellationToken ct)
    {
        var order = await orders.CreateOrderAsync(request, ct);
        return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateOrderStatusRequest request, CancellationToken ct)
    {
        await orders.UpdateStatusAsync(id, request.Status, ct); return NoContent();
    }

    [HttpPost("{id:guid}/payment-proof")]
    public async Task<IActionResult> SubmitProof(Guid id, SubmitPaymentProofRequest request, CancellationToken ct)
    {
        await orders.SubmitPaymentProofAsync(id, request, ct); return NoContent();
    }
}
