namespace Domain.Entities;

public class ProductGroup : BaseEntity
{
    public Guid Id { get; set; }
    public List<Product> Products { get; set; } = [];
    public List<ProductGroupLocalization> Localizations { get; set; } = [];
}
