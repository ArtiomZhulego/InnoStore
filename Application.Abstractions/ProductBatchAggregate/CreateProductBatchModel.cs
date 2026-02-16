using Application.Abstractions.ProductGroupAggregate;

namespace Application.Abstractions.ProductBatchAggregate;

public sealed class CreateProductBatchModel
{
    public required IEnumerable<ProductModel> Products { get; set; }
    public required CreateProductCategoryModel ProductGroup { get; set; }
}
