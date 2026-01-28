using System.Reflection;
using Application.Abstractions.ProductSizeAggregate;
using Domain.Entities;
using Shared.Constants;

namespace Application.Mappers;

public static class ProductSizeMapper
{
   public static ProductSizeDTO ToDTO(this ProductSize productSize)
   {
       return new ProductSizeDTO
       {
           Id = productSize.Id,
           Size = productSize.Localizations.FirstOrDefault()?.Name ?? LocalizationConstants.DefaultTranslation,
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
            Localizations = [.. productSizeDTO.Localizations.Select(loc => loc.ToEntity(id))]
        };
    }

    public static ProductSize UpdateEntity(this UpdateProductSizeModel productSizeDTO, ProductSize productSize)
    {
        foreach (var localizationModel in productSizeDTO.Localizations)
        {
            var localization = productSize.Localizations
                .FirstOrDefault(x => x.LanguageISOCode == localizationModel.LanguageISOCode);

            if (localization is not null)
            {
                localization.Name = localizationModel.Name;
            }
            else
            {
                productSize.Localizations.Add(localizationModel.ToEntity(productSize.Id));
            }
        }
        return productSize;
    }
}
