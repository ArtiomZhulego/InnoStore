using Application.Abstractions.ProductImageAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductImageMapper
{
    extension(ProductImage productImage)
    {
        public ProductImageDTO ToDTO()
        {
            return new ProductImageDTO
            {
                Id = productImage.Id,
                ImageUrl = productImage.ImageUrl,
                ProductId = productImage.ProductId
            };
        }
    }

    extension(CreateProductImageModel createProductImage)
    {
        public ProductImage ToEntity(Guid productId)
        {
            return new ProductImage
            {
                ImageUrl = createProductImage.ImageUrl,
                ProductId = productId
            };
        }
    }

    extension(UpdateProductImageModel updateProductImage)
    {
        public ProductImage UpdateEntity(ProductImage productImage)
        {
            productImage.ImageUrl = updateProductImage.ImageUrl;
            return productImage;
        }
    }
}
