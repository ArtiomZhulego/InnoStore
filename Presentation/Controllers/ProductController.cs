using Application.Abstractions.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.Products.Controller)]
public sealed class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet(PathConstants.Products.GetById)]
    [ProducesResponseType(typeof(ProductDTO), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetProductById(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        var product = await productService.GetByIdAsync(id, languageCode, cancellationToken);
        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDTO), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel createProductModel, CancellationToken cancellationToken)
    {
        var product = await productService.CreateAsync(createProductModel, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id, languageCode = "en" }, product);
    }

    [HttpPut(PathConstants.Products.Update)]
    [ProducesResponseType(typeof(ProductDTO), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid productId,[FromBody] UpdateProductModel updateProductModel, CancellationToken cancellationToken)
    {
        var product = await productService.UpdateAsync(productId, updateProductModel, cancellationToken);
        return Ok(product);
    }

    [HttpDelete(PathConstants.Products.Delete)]
    [ProducesResponseType( 204)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId, CancellationToken cancellationToken)
    {
        await productService.DeleteAsync(productId, cancellationToken);
        return NoContent();
    }

    [HttpGet(PathConstants.Products.GetByGroup)]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetProducts(Guid groupId, string languageCode, CancellationToken cancellationToken)
    {
        var product = await productService.GetByGroupAsync(groupId, languageCode, cancellationToken);
        return Ok(product);
    }
}
