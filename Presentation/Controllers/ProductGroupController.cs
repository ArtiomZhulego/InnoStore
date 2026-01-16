using Application.Abstractions.ProductAggregate;
using Application.Abstractions.ProductGroupAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductGroupController
{
    private readonly IProductGroupService _productGroupService;

    public ProductGroupController(IProductGroupService productGroupService)
    {
        _productGroupService = productGroupService;
    }

    [HttpGet("{languageCode}/{id}")]
    [ProducesResponseType(typeof(ProductGroupDTO), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetProductGroupById(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        var productGroup = await _productGroupService.GetByIdAsync(id, languageCode, cancellationToken);
        return new OkObjectResult(productGroup);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductGroupDTO), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> CreateProductGroup([FromBody] CreateProductGroupModel createProductGroupModel, CancellationToken cancellationToken)
    {
        var productGroup = await _productGroupService.CreateAsync(createProductGroupModel, cancellationToken);
        return new CreatedAtActionResult(nameof(GetProductGroupById), "ProductGroup", new { id = productGroup.Id, languageCode = "en" }, productGroup);
    }

    [HttpPut("{productGroupId}")]
    [ProducesResponseType(typeof(ProductGroupDTO), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> UpdateProductGroup([FromRoute] Guid productGroupId, [FromBody] UpdateProductGroupModel updateProductGroupModel, CancellationToken cancellationToken)
    {
        var productGroup = await _productGroupService.UpdateAsync(productGroupId, updateProductGroupModel, cancellationToken);
        return new OkObjectResult(productGroup);
    }

    [HttpDelete("{productGroupId}")]
    [ProducesResponseType( 204)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> DeleteProductGroup([FromRoute] Guid productGroupId, CancellationToken cancellationToken)
    {
        await _productGroupService.DeleteAsync(productGroupId, cancellationToken);
        return new NoContentResult();
    }

    [HttpGet("{languageCode}")]
    [ProducesResponseType(typeof(IEnumerable<ProductGroupDTO>), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetAllProductGroups(string languageCode, CancellationToken cancellationToken)
    {
        var productGroups = await _productGroupService.GetAllAsync(languageCode, cancellationToken);
        return new OkObjectResult(productGroups);
    }
}
