namespace Application.Abstractions.ProductAggregate;

public sealed class ProductLocalizationModel
{
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;

    public required string LanguageISOCode { get; set; }
}
