using Application.Abstractions.ProductGroupAggregate;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductGroupMapper
{
    extension(ProductGroup productGroup)
    {
        public ProductGroupDTO ToDTO()
        {
            return new ProductGroupDTO
            {
                Id = productGroup.Id,
                Name = productGroup.Localizations.First().Name,
                Products = productGroup.Products.Select(x => x.ToInformation())
            };
        }

        public ProductGroupInformation ToInformation()
        {
            return new ProductGroupInformation
            {
                Id = productGroup.Id,
                Name = productGroup.Localizations.First().Name
            };
        }
    }

    public static ProductGroup ToEntity(this CreateProductGroupModel model)
    {
        var id = Guid.NewGuid();    
        return new ProductGroup
        {
            Id = id,
            Localizations = [.. model.Localizations.Select(x => x.ToEntity(id))]
        };
    }

    public static ProductGroup UpdateEntity(this UpdateProductGroupModel model, ProductGroup productGroup)
    {
        foreach (var localizationModel in model.Localizations)
        {
            var localization = productGroup.Localizations
                .FirstOrDefault(x => x.LanguageISOCode == localizationModel.LanguageISOCode);
            
            if (localization is not null)
            {
                localization.Name = localizationModel.Name;
            }
            else
            {
                productGroup.Localizations.Add(localizationModel.ToEntity(productGroup.Id));
            }
        }

        return productGroup;
    }
}
