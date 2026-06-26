using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextilSalas.Application.Abstractions;
using TextilSalas.Application.DTOs;

namespace TextilSalas.Api.Controllers;
[ApiController]
[Route("api/quotations")]
public sealed class QuotationsController(IQuotationRepository quotations) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<QuotationDto>> Create(CreateQuotationRequest request, CancellationToken ct) => Ok(await quotations.CreateAsync(request, ct));

    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<QuotationDto>>> Get(CancellationToken ct) => Ok(await quotations.GetAllAsync(ct));
}
