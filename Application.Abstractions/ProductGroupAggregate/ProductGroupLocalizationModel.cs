namespace Application.Abstractions.ProductGroupAggregate;

public sealed class ProductGroupLocalizationModel
{
    public required string Name { get; set; } = string.Empty;

    public required string LanguageISOCode { get; set; }
}
