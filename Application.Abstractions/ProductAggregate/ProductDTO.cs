using Application.Abstractions.ProductGroupAggregate;
using Application.Abstractions.ProductImageAggregate;
using Application.Abstractions.ProductSizeAggregate;

namespace Application.Abstractions.ProductAggregate;

public sealed class ProductDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required decimal Price { get; set; }
    public required Guid ProductGroupId { get; set; }
    public ProductGroupInformation? ProductGroup { get; set; }
    public IEnumerable<ProductImageDTO> Images { get; set; } = [];
    public IEnumerable<ProductSizeDTO> Sizes { get; set; } = [];
}
