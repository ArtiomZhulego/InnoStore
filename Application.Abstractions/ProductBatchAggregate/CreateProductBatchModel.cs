using Application.Abstractions.ProductCategoryAggregate;

namespace Application.Abstractions.ProductBatchAggregate;

public sealed class CreateProductBatchModel
{
    public required IEnumerable<ProductModel> Products { get; set; }
    public required CreateProductCategoryModel ProductCategory { get; set; }
}
