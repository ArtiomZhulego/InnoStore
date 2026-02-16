using Application.Abstractions.ProductAggregate;
using Application.Abstractions.ProductBatchAggregate;
using Application.Abstractions.ProductGroupAggregate;
using Application.Mappers;

namespace Application.Services;

public sealed class ProductBatchService : IProductBatchService
{
    private readonly IProductService _productService;
    private readonly IProductCategoryService _productGroupService;

    public ProductBatchService(IProductService productService, IProductCategoryService productGroupService)
    {
        _productService = productService;
        _productGroupService = productGroupService;
    }

    public async Task<IEnumerable<ProductDTO>> CreateBatchAsync(CreateProductBatchModel createProductModel, CancellationToken cancellationToken = default)
    {
        var productGroup = await _productGroupService.CreateAsync(createProductModel.ProductGroup, cancellationToken);

        var products = new List<ProductDTO>();
        foreach (var product in createProductModel.Products)
        {
            var createModel = product.ToCreateModel(productGroup.Id);
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
