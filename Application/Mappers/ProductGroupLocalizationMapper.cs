using Application.Abstractions.ProductGroupAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductGroupLocalizationMapper
{
    extension(ProductGroupLocalizationModel localization)
    {
        public ProductGroupLocalization ToEntity(Guid productGroupId)
        {
            return new ProductGroupLocalization
            {
                Id = Guid.NewGuid(),
                ProductGroupId = productGroupId,
                LanguageISOCode = localization.LanguageISOCode,
                Name = localization.Name,
            };
        }
    }
}
