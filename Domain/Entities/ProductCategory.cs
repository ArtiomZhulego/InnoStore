namespace Domain.Entities;

public class ProductCategory : BaseEntity
{
    public Guid Id { get; set; }
    
    public List<Product> Products { get; set; } = [];
    
    public List<ProductCategoryLocalization> Localizations { get; set; } = [];
}
