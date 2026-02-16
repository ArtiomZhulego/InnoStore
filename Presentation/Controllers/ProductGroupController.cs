using Application.Abstractions.ProductGroupAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.ProductGroups.Controller)]
public class ProductGroupController(IProductCategoryService productGroupService) : ControllerBase
{
    [HttpGet(PathConstants.ProductGroups.GetById, Name = "getGroupById")]
    [ProducesResponseType(typeof(ProductCategoryDTO), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetProductGroupById(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupService.GetByIdAsync(id, languageCode, cancellationToken);
        return Ok(productGroup);
    }

    [HttpPost(Name = "createGroup")]
    [ProducesResponseType(typeof(ProductCategoryDTO), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> CreateProductGroup([FromBody] CreateProductCategoryModel createProductGroupModel, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupService.CreateAsync(createProductGroupModel, cancellationToken);
        return CreatedAtAction(nameof(GetProductGroupById), "ProductGroup", new { id = productGroup.Id, languageCode = "en" }, productGroup);
    }

    [HttpPut(PathConstants.ProductGroups.Update, Name = "updateGroup")]
    [ProducesResponseType(typeof(ProductCategoryDTO), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> UpdateProductGroup([FromRoute] Guid productGroupId, [FromBody] UpdateProductCategoryModel updateProductGroupModel, CancellationToken cancellationToken)
    {
        var productGroup = await productGroupService.UpdateAsync(productGroupId, updateProductGroupModel, cancellationToken);
        return Ok(productGroup);
    }

    [HttpDelete(PathConstants.ProductGroups.Delete, Name = "deleteGroup")]
    [ProducesResponseType( 204)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> DeleteProductGroup([FromRoute] Guid productGroupId, CancellationToken cancellationToken)
    {
        await productGroupService.DeleteAsync(productGroupId, cancellationToken);
        return NoContent();
    }

    [HttpGet(PathConstants.ProductGroups.GetAll, Name = "getAll")]
    [ProducesResponseType(typeof(IEnumerable<ProductCategoryDTO>), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetAllProductGroups(string languageCode, CancellationToken cancellationToken)
    {
        var productGroups = await productGroupService.GetAllAsync(languageCode, cancellationToken);
        return Ok(productGroups);
    }
}
