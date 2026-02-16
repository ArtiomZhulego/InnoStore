using Application.Abstractions.ProductColorAggregate;
using Application.Abstractions.ProductSizeAggregate;

namespace Application.Abstractions.ProductAggregate;

public sealed class UpdateProductModel
{
    public required decimal Price { get; set; }
    
    public required Guid ProductCategoryId { get; set; }
    
    public required IEnumerable<ProductLocalizationModel> Localizations { get; set; }
    public required IEnumerable<UpdateProductSizeModel> Sizes { get; set; }
    public required IEnumerable<UpdateProductColorModel> Colors { get; set; }
}
