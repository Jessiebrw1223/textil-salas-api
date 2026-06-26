using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextilSalas.Application.Abstractions;
using TextilSalas.Application.DTOs;

namespace TextilSalas.Api.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(IProductRepository products) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get([FromQuery] bool includeInactive = false, CancellationToken ct = default) => Ok(await products.GetProductsAsync(includeInactive, ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken ct)
    {
        var product = await products.GetProductAsync(id, ct);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> Categories(CancellationToken ct) => Ok(await products.GetCategoriesAsync(ct));

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<ActionResult> Create(UpsertProductRequest request, CancellationToken ct)
    {
        var id = await products.CreateProductAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpsertProductRequest request, CancellationToken ct)
    {
        await products.UpdateProductAsync(id, request, ct); return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await products.DeleteProductAsync(id, ct); return NoContent();
    }
}
