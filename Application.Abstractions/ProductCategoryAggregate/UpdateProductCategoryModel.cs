namespace Application.Abstractions.ProductGroupAggregate;

public sealed class UpdateProductCategoryModel
{
    public IEnumerable<ProductCategoryLocalizationModel> Localizations { get; set; } = [];
}
