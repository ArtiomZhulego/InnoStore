using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal class ProductGroupConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasMany(p => p.Localizations)
           .WithOne(pl => pl.ProductGroup)
           .HasForeignKey(pl => pl.ProductGroupId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
