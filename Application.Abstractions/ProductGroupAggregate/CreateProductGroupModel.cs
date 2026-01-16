namespace Application.Abstractions.ProductGroupAggregate;

public sealed class CreateProductGroupModel
{
    public IEnumerable<ProductGroupLocalizationModel> Localizations { get; set; } = [];
}
