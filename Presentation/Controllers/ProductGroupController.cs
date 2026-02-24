using Application.Abstractions.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.Models.ErrorModels;

namespace Presentation.Controllers;

[ApiController]
[Route(PathConstants.ProductCategories.Controller)]
public class ProductCategoryController(IProductCategoryService productCategoryService) : ControllerBase
{
    [HttpGet(PathConstants.ProductCategories.GetById, Name = "getCategoryById")]
    [ProducesResponseType(typeof(ProductCategoryDTO), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetProductCategoryById(Guid id, string languageCode, CancellationToken cancellationToken)
    {
        var productCategory = await productCategoryService.GetByIdAsync(id, languageCode, cancellationToken);
        return Ok(productCategory);
    }

    [HttpPost(Name = "createCategory")]
    [ProducesResponseType(typeof(ProductCategoryDTO), 201)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategoryModel createProductCategoryModel, CancellationToken cancellationToken)
    {
        var productCategory = await productCategoryService.CreateAsync(createProductCategoryModel, cancellationToken);
        return CreatedAtAction(nameof(GetProductCategoryById), "ProductCategory", new { id = productCategory.Id, languageCode = "en" }, productCategory);
    }

    [HttpPut(PathConstants.ProductCategories.Update, Name = "updateCategory")]
    [ProducesResponseType(typeof(ProductCategoryDTO), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> UpdateProductCategory([FromRoute] Guid productCategoryId, [FromBody] UpdateProductCategoryModel updateProductCategoryModel, CancellationToken cancellationToken)
    {
        var productCategory = await productCategoryService.UpdateAsync(productCategoryId, updateProductCategoryModel, cancellationToken);
        return Ok(productCategory);
    }

    [HttpDelete(PathConstants.ProductCategories.Delete, Name = "deleteCategory")]
    [ProducesResponseType( 204)]
    [ProducesResponseType(typeof(ErrorDetails), 400)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> DeleteProductCategory([FromRoute] Guid productCategoryId, CancellationToken cancellationToken)
    {
        await productCategoryService.DeleteAsync(productCategoryId, cancellationToken);
        return NoContent();
    }

    [HttpGet(PathConstants.ProductCategories.GetAll, Name = "getAll")]
    [ProducesResponseType(typeof(IEnumerable<ProductCategoryInformation>), 200)]
    [ProducesResponseType(typeof(ErrorDetails), 404)]
    [ProducesResponseType(typeof(ErrorDetails), 500)]
    public async Task<IActionResult> GetAllProductCategorys(CancellationToken cancellationToken)
    {
        var productCategorys = await productCategoryService.GetAllAsync(cancellationToken);
        return Ok(productCategorys);
    }
}
