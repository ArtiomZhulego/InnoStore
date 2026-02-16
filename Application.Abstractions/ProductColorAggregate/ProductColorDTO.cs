using Application.Abstractions.ProductImageAggregate;
using Application.Abstractions.ProductSizeAggregate;

namespace Application.Abstractions.ProductColorAggregate;

public class ProductColorDTO
{
    public Guid Id { get; set; }

    public required string Color { get; set; }

    public List<ProductImageDTO> Images { get; set; } = [];
}
