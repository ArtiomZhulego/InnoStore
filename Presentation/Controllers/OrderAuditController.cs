using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.OrderAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.OrderAudits.Controller)]
public sealed class OrderAuditController(IOrderAuditService orderAuditService) : ControllerBase
{
    [HttpGet(PathConstants.OrderAudits.GetByOrderId)]
    [ProducesResponseType(typeof(IEnumerable<OrderAuditDto>), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetOrderAuditsByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        var audits = await orderAuditService.GetAuditByOfferAsync(orderId, cancellationToken);
        return Ok(audits);
    }
}