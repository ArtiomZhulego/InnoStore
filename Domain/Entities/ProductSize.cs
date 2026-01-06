namespace Domain.Entities;

public class ProductSize : BaseEntity
{
    public Guid Id { get; set; }
    public required string Size { get; set; } = string.Empty;
    public required Guid ProductId { get; set; }
    public Product? Product { get; set; }
}
