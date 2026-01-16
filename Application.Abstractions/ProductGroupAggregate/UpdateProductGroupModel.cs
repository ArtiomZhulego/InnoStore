namespace Application.Abstractions.ProductGroupAggregate;

public class UpdateProductGroupModel
{
    public IEnumerable<ProductGroupLocalizationModel> Localizations { get; set; } = [];
}
