using Application.Abstractions.ProductImageAggregate;

namespace Application.Abstractions.ProductColorAggregate;

public class UpdateProductColorModel
{
    public required string Color { get; set; }

    public List<CreateProductImageModel> Images { get; set; } = [];
}
