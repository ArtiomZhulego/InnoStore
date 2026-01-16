namespace Application.Abstractions.ProductSizeAggregate;

public sealed class ProductSizeLocalizationModel 
{
    public required string Name { get; set; } = string.Empty;

    public required string LanguageISOCode { get; set; }
}
