using Application.Abstractions.ProductAggregate;
using Application.Abstractions.ProductColorAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductColorMapper
{
    extension(ProductColor productColor)
    {
        public ProductColorDTO ToDTO()
        {
            return new ProductColorDTO
            {
                Id = productColor.Id,
                Color = productColor.Color,
                Images = productColor.Images.Select(i => i.ToDTO()).ToList()
            };
        }
    }

    extension(CreateProductColorModel model)
    {
        public ProductColor ToEntity(Guid productId)
        {
            var id = Guid.NewGuid();
            return new ProductColor
            {
                Id = id,
                ProductId = productId,
                Color = model.Color,
                Images = model.Images.Select(i => i.ToEntity(id)).ToList()
            };
        }
    }
}
