namespace Domain.Entities;

public class ProductGroup : BaseEntity
{
    public Guid Id { get; set; }
    public IEnumerable<Product> Products { get; set; } = [];
    public IEnumerable<ProductGroupLocalization> Localizations { get; set; } = [];
}
