namespace Domain.Entities;

public class ProductSizaLocalization
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required Guid ProductSizeId { get; set; }
    public ProductSize? ProductSize { get; set; }

    /// <summary>
    /// Two-letter ISO language code (e.g., "en" for English, "fr" for French).
    /// For more information, https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes
    /// </summary>
    public required string LanguageISOCode { get; set; }
}
