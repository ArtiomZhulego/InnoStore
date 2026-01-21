using Application.Abstractions.ProductAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductLocalizationMapper
{
    public static ProductLocalization ToEntity(this ProductLocalizationModel model, Guid productId)
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
