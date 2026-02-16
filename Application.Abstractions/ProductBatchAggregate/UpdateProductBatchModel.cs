using Application.Abstractions.ProductCategoryAggregate;

namespace Application.Abstractions.ProductBatchAggregate;

public sealed class UpdateProductBatchModel
{
    public required IEnumerable<ProductModel> Products { get; set; }
    public required UpdateProductCategoryModel ProductCategory { get; set; }
}
