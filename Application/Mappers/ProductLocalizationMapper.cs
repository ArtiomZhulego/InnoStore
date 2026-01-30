using Application.Abstractions.ProductAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductLocalizationMapper
{
    extension(ProductLocalizationModel model)
    {
        public ProductLocalization ToEntity(Guid productId)
        {
            return new ProductLocalization
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                LanguageISOCode = model.LanguageISOCode,
                Description = model.Description,
                Name = model.Name
            };
        }
    }
}
