namespace Application.Abstractions.ProductCategoryAggregate;

public sealed class CreateProductCategoryModel
{
    public IEnumerable<ProductCategoryLocalizationModel> Localizations { get; set; } = [];
}
