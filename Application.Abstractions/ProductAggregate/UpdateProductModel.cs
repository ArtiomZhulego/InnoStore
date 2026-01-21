using Application.Abstractions.ProductSizeAggregate;

namespace Application.Abstractions.ProductAggregate;

public sealed class UpdateProductModel
{
    public required decimal Price { get; set; }
    
    public required Guid ProductGroupId { get; set; }
    
    public required IEnumerable<ProductLocalizationModel> Localizations { get; set; }
}
