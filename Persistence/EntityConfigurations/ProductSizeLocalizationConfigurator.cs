using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductSizeLocalizationConfigurator : IEntityTypeConfiguration<ProductSizeLocalization>
{
    public void Configure(EntityTypeBuilder<ProductSizeLocalization> builder)
    {
        builder.HasIndex(x => new { x.ProductSizeId, x.LanguageISOCode }).IsUnique();
    }
}
