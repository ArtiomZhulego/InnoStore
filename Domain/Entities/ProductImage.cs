namespace Domain.Entities;

public class ProductImage : BaseEntity
{
    public Guid Id { get; set; }
    public required string ImageUrl { get; set; } = string.Empty;
    public required Guid ProductId { get; set; }
    public Product? Product { get; set; }
}
