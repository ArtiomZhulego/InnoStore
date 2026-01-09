using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductSizeLocalizationConfigurator : IEntityTypeConfiguration<ProductSizaLocalization>
{
    public void Configure(EntityTypeBuilder<ProductSizaLocalization> builder)
    {
        builder.HasIndex(x => new { x.ProductSizeId, x.LanguageISOCode }).IsUnique();
    }
}
