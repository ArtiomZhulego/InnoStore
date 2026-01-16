using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.ProductGroup)
               .WithMany(pg => pg.Products)
               .HasForeignKey(p => p.ProductGroupId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Localizations)
               .WithOne(pl => pl.Product)
               .HasForeignKey(pl => pl.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
