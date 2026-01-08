namespace Domain.Entities;

public class ProductSizaLocalization
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required Guid ProductSizeId { get; set; }
    public ProductSize? ProductSize { get; set; }
    public required Guid LanguageId { get; set; }
    public Language? Language { get; set; }
}
