namespace Domain.Entities;

public class ProductLocalization
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public required Guid LanguageId { get; set; }
    public Language? Language { get; set; }
}
