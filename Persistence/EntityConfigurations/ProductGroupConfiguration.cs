using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.HasMany(p => p.Localizations)
           .WithOne(pl => pl.ProductGroup)
           .HasForeignKey(pl => pl.ProductGroupId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
