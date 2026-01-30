using System.Reflection;
using Application.Abstractions.ProductSizeAggregate;
using Application.Constants;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductSizeMapper
{
    extension(ProductSize productSize)
    {
        public ProductSizeDTO ToDTO()
        {
            return new ProductSizeDTO
            {
                Id = productSize.Id,
                Size = productSize.Localizations.FirstOrDefault()?.Name ?? LocalizationConstants.DefaultTranslation,
                ProductId = productSize.ProductId
            };
        }
    }

    extension(CreateProductSizeModel productSizeDTO)
    {
        public ProductSize ToEntity(Guid productId)
        {
            var id = Guid.NewGuid();
            return new ProductSize
            {
                Id = id,
                ProductId = productId,
                Localizations = [.. productSizeDTO.Localizations.Select(loc => loc.ToEntity(id))]
            };
        }
    }

    extension(UpdateProductSizeModel productSizeDTO)
    {
        public ProductSize UpdateEntity(ProductSize productSize)
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
}
