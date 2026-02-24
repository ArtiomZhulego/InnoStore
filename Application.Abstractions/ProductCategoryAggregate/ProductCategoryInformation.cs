namespace Application.Abstractions.ProductCategoryAggregate;

public sealed class ProductCategoryInformation
{
    public Guid Id { get; set; }
    
    public required IEnumerable<ProductCategoryLocalizationModel> Localizations { get; set; }
}
