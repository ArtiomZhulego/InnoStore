namespace Application.Abstractions.ProductImageAggregate;

public sealed class CreateProductImage
{
    public required Stream FileStream { get; set; }
    public required string FileName { get; set; }
    public required string ContentType { get; set; }
}
