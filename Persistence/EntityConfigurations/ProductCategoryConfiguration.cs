using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasMany(p => p.Localizations)
           .WithOne(pl => pl.ProductCategory)
           .HasForeignKey(pl => pl.ProductCategoryId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
