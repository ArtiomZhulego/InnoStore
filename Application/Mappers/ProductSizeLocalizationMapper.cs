using Application.Abstractions.ProductSizeAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductSizeLocalizationMapper
{
    public static ProductSizeLocalizationModel ToDTO(this ProductSizeLocalization productSizeLocalization)
    {
        return new ProductSizeLocalizationModel
        {
            Name = productSizeLocalization.Name,
            LanguageISOCode = productSizeLocalization.LanguageISOCode
        };
    }

    public static ProductSizeLocalization ToEntity(this ProductSizeLocalizationModel productSizeLocalization, Guid productSizeId)
    {
        return new ProductSizeLocalization
        {
            Id = Guid.NewGuid(),
            ProductSizeId = productSizeId,
            Name = productSizeLocalization.Name,
            LanguageISOCode = productSizeLocalization.LanguageISOCode
        };
    }
}
