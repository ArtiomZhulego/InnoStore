namespace Domain.Entities;

public class ProductSize : BaseEntity
{
    public Guid Id { get; set; }
    
    public required Guid ProductId { get; set; }
    
    public Product? Product { get; set; }
    
    public List<ProductSizeLocalization> Localizations { get; set; } = [];
}
