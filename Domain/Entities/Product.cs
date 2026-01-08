namespace Domain.Entities;

public class Product : BaseEntity
{
    public Guid Id { get; set; }
    public required decimal Price { get; set; }
    public required Guid ProductGroupId { get; set; }
    public ProductGroup? ProductGroup { get; set; }
    public List<ProductImage> Images { get; set; } = [];
    public List<ProductSize> Sizes { get; set; } = [];
    public List<ProductLocalization> Localizations { get; set; } = [];
}
