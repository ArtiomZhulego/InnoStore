namespace Domain.Entities;

public class ProductGroup : BaseEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
}
