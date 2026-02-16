using Application.Abstractions.ProductColorAggregate;
using Application.Abstractions.ProductGroupAggregate;
using Application.Abstractions.ProductSizeAggregate;

namespace Application.Abstractions.ProductAggregate;

public sealed class ProductDTO
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required decimal Price { get; set; }

    public required Guid ProductCategoryId { get; set; }
    
    public ProductCategoryInformation? ProductCategory { get; set; }
    
    public IEnumerable<ProductColorDTO> Colors { get; set; } = [];

    public IEnumerable<ProductSizeDTO> Sizes { get; set; } = [];
}
