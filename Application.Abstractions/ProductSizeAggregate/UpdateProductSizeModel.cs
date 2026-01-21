namespace Application.Abstractions.ProductSizeAggregate;

public sealed class UpdateProductSizeModel
{
    public required IEnumerable<ProductSizeLocalizationModel> Localizations { get; set; }
    
    public required string Size { get; set; } = string.Empty;
}
