namespace Application.Abstractions.ProductCategoryAggregate;

public sealed class UpdateProductCategoryModel
{
    public IEnumerable<ProductCategoryLocalizationModel> Localizations { get; set; } = [];
}
