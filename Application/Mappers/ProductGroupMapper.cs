using Application.Abstractions.ProductGroupAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductGroupMapper
{
    public static ProductGroupDTO ToDTO(this ProductGroup productGroup)
    {
        return new ProductGroupDTO
        {
            Id = productGroup.Id,
            Name = productGroup.Localizations.First().Name
        };
    }
}
