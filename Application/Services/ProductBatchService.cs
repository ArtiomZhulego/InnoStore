using Application.Abstractions.ProductAggregate;
using Application.Abstractions.ProductBatchAggregate;
using Application.Abstractions.ProductCategoryAggregate;
using Application.Mappers;

namespace Application.Services;

public sealed class ProductBatchService : IProductBatchService
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _productCategoryService;

    public ProductBatchService(IProductService productService, IProductCategoryService productCategoryService)
    {
        _productService = productService;
        _productCategoryService = productCategoryService;
    }

    public async Task<IEnumerable<ProductDTO>> CreateBatchAsync(CreateProductBatchModel createProductModel, CancellationToken cancellationToken = default)
    {
        var productCategory = await _productCategoryService.CreateAsync(createProductModel.ProductCategory, cancellationToken);

        var products = new List<ProductDTO>();
        foreach (var product in createProductModel.Products)
        {
            var createModel = product.ToCreateModel(productCategory.Id);
            var createdProduct = await _productService.CreateAsync(createModel, cancellationToken);
            products.Add(createdProduct);
        }

        return products;
    }

    public Task<IEnumerable<ProductDTO>> UpdateBatchAsync(Guid id, UpdateProductBatchModel updateProductBatchModel, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
