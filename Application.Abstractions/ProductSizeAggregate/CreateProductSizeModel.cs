namespace Application.Abstractions.ProductSizeAggregate;

public sealed class CreateProductSizeModel
{
    public required IEnumerable<ProductSizeLocalizationModel> Localizations { get; set; }
}
