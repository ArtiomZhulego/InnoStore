using Application.Abstractions.ProductAggregate;

namespace Application.Abstractions.ProductImageAggregate;

public sealed class ProductImageDTO
{
    public Guid Id { get; set; }
    
    public required string ImageUrl { get; set; } = string.Empty;
    
    public required Guid ProductColorId { get; set; }
}
