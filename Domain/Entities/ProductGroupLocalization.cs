namespace Domain.Entities;

public class ProductGroupLocalization
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required Guid ProductGroupId { get; set; }
    public ProductGroup? ProductGroup { get; set; }
    public required Guid LanguageId { get; set; }
    public Language? Language { get; set; }
}
