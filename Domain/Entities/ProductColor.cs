namespace Domain.Entities;

public sealed class ProductColor : BaseEntity
{
    public Guid Id { get; set; }

    public required string Color { get; set; }

    public required Guid ProductId { get; set; }

    public Product? Product { get; set; }

    public List<ProductImage> Images { get; set; } = [];
}
