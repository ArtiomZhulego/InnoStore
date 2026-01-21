using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductLocalizationConfigurator : IEntityTypeConfiguration<ProductLocalization>
{
    public void Configure(EntityTypeBuilder<ProductLocalization> builder)
    {
        builder.HasIndex(x => new { x.ProductId, x.LanguageISOCode }).IsUnique();
    }
}
