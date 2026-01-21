namespace Application.Abstractions.ProductGroupAggregate;

public sealed class UpdateProductGroupModel
{
    public IEnumerable<ProductGroupLocalizationModel> Localizations { get; set; } = [];
}
