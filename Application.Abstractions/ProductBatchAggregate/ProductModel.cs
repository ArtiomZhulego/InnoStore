using Application.Abstractions.ProductAggregate;
using Application.Abstractions.ProductColorAggregate;
using Application.Abstractions.ProductSizeAggregate;

namespace Application.Abstractions.ProductBatchAggregate;

public sealed class ProductModel
{
    public required decimal Price { get; set; }

    public required IEnumerable<ProductLocalizationModel> Localizations { get; set; }
    public required IEnumerable<CreateProductSizeModel> Sizes { get; set; }
    public required IEnumerable<CreateProductColorModel> Colors { get; set; }
}
