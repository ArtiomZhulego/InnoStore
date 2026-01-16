using Application.Abstractions.ProductSizeAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductSizeMapper
{
   public static ProductSizeDTO ToDTO(this ProductSize productSize)
   {
       return new ProductSizeDTO
       {
           Id = productSize.Id,
           Size = productSize.Size,
           ProductId = productSize.ProductId
       };
    }

    public static ProductSize ToEntity(this CreateProductSizeModel productSizeDTO, Guid productId)
    {
        var id = Guid.NewGuid();
        return new ProductSize
        {
            Id = id,
            ProductId = productId,
            Size = productSizeDTO.Size,
            Localizations = [.. productSizeDTO.Localizations.Select(loc => loc.ToEntity(id))]
        };
    }
}
