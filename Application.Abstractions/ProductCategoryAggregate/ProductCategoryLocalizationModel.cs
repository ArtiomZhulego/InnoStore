namespace Application.Abstractions.ProductCategoryAggregate;

public sealed class ProductCategoryLocalizationModel
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string LanguageISOCode { get; set; }
}
