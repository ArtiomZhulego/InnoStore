using Application.Abstractions.DTOs.Entities;
using Application.Abstractions.ProductQuantittyAggreate;
using Application.Abstractions.ProductQuantittyAggreate.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.ProductQuantity.Controller)]
internal sealed class ProductQuantityController(IProductQuantityService inventoryService) : ControllerBase
{
    [HttpPost(PathConstants.ProductQuantity.Add)]
    [ProducesResponseType(typeof(ProductQuantityTransactionDto), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> SupplyQuantity([FromBody] AddProductQuantityModel model, CancellationToken cancellationToken)
    {
        var transaction = await inventoryService.SupplyQuantityAsync(model, cancellationToken);
        return Ok(transaction);
    }

    [HttpGet(PathConstants.ProductQuantity.GetAvailable)]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetAvailableQuantity(Guid productSizeId, CancellationToken cancellationToken)
    {
        var quantity = await inventoryService.GetAvailableQuantityAsync(productSizeId, cancellationToken);
        return Ok(quantity);
    }

    [HttpGet(PathConstants.ProductQuantity.GetHistory)]
    [ProducesResponseType(typeof(IEnumerable<ProductQuantityTransactionDto>), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetChangeHistoryQuantity(Guid productSizeId, CancellationToken cancellationToken)
    {
        var quantity = await inventoryService.GetChangeHistoryAsync(productSizeId, cancellationToken);
        return Ok(quantity);
    }
}