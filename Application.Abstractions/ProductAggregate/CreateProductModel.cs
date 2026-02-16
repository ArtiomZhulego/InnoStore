using Application.Abstractions.ProductColorAggregate;
using Application.Abstractions.ProductSizeAggregate;

namespace Application.Abstractions.ProductAggregate;

public sealed class CreateProductModel
{
    public required decimal Price { get; set; }
    
    public required Guid ProductCategoryId { get; set; }
    
    public required IEnumerable<ProductLocalizationModel> Localizations { get; set; }
    public required IEnumerable<CreateProductSizeModel> Sizes { get; set; }
    public required IEnumerable<CreateProductColorModel> Colors { get; set; }
}
