namespace Domain.Entities;

public class ProductImage : BaseEntity
{
    public Guid Id { get; set; }
    
    public required string ImageUrl { get; set; } 

    public required int OrderNumber { get; set; }

    public required Guid ProductColorId { get; set; }
    
    public ProductColor? ProductColor { get; set; }
}
