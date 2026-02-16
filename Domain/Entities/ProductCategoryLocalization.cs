namespace Domain.Entities;

public class ProductCategoryLocalization
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required Guid ProductCategoryId { get; set; }
    
    public ProductCategory? ProductCategory { get; set; }
    
    /// <summary>
    /// Two-letter ISO language code (e.g., "en" for English, "fr" for French).
    /// For more information, https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes
    /// </summary>
    public required string LanguageISOCode { get; set; }
}
