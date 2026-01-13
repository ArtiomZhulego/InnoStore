using Application.Abstractions.ProductAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductLocalizationMapper
{
    public static ProductLocalization ToEntity(this ProductLocalizationModel model, Guid productId)
    {
        var id = Guid.NewGuid();
        return new ProductLocalization
        {
            Id = id,
            ProductId = productId,
            LanguageISOCode = model.LanguageISOCode,
            Name = model.Name
        };
    }
}
