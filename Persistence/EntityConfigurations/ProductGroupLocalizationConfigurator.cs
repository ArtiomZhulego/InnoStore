using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductGroupLocalizationConfigurator : IEntityTypeConfiguration<ProductGroupLocalization>
{
    public void Configure(EntityTypeBuilder<ProductGroupLocalization> builder)
    {
        builder.HasIndex(x => new { x.ProductGroupId, x.LanguageISOCode }).IsUnique();
    }
}
