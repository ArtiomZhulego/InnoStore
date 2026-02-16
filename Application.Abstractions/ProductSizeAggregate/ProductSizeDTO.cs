using Application.Abstractions.ProductAggregate;

namespace Application.Abstractions.ProductSizeAggregate;

public sealed class ProductSizeDTO
{
    public Guid Id { get; set; }
    
    public required string Size { get; set; } = string.Empty;
    
    public required Guid ProductColorId { get; set; }
}
