using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductCategoryLocalizationConfigurator : IEntityTypeConfiguration<ProductCategoryLocalization>
{
    public void Configure(EntityTypeBuilder<ProductCategoryLocalization> builder)
    {
        builder.HasIndex(x => new { x.ProductCategoryId, x.LanguageISOCode }).IsUnique();
    }
}
