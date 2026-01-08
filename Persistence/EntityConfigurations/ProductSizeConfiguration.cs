using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
{
    public void Configure(EntityTypeBuilder<ProductSize> builder)
    {
        builder.HasOne<Product>()
               .WithMany(p => p.Sizes)
               .HasForeignKey(ps => ps.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(ps => ps.Localizations)
            .WithOne(pl => pl.ProductSize)
            .HasForeignKey(pl => pl.ProductSizeId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
