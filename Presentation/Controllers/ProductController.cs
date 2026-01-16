using Application.Abstractions.ProductAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{languageCode}/{id}")]
    [ProducesResponseType(typeof(ProductDTO), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetProductById(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, languageCode, cancellationToken);
        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDTO), 201)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel createProductModel, CancellationToken cancellationToken)
    {
        var product = await _productService.CreateAsync(createProductModel, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id, languageCode = "en" }, product);
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(ProductDTO), 201)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid productId,[FromBody] UpdateProductModel updateProductModel, CancellationToken cancellationToken)
    {
        var product = await _productService.UpdateAsync(productId, updateProductModel, cancellationToken);
        return Ok(product);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(productId, cancellationToken);
        return NoContent();
    }

    [HttpGet("{languageCode}/group/{groupId}")]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetProducts(Guid groupId,string languageCode, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByGroupAsync(groupId, languageCode, cancellationToken);
        return Ok(product);
    }
}
