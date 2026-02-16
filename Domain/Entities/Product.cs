namespace Domain.Entities;

public class Product : BaseEntity
{
    public Guid Id { get; set; }
    
    public required decimal Price { get; set; }

    public required Guid ProductCategoryId { get; set; }
    
    public ProductCategory? ProductCategory { get; set; }
    
    public List<ProductColor> Colors { get; set; } = [];
    
    public List<ProductSize> Sizes { get; set; } = [];

    public List<ProductLocalization> Localizations { get; set; } = [];
}
