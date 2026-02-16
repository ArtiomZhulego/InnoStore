using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasOne(i => i.ProductColor)
               .WithMany(p => p.Images)
               .HasForeignKey(pi => pi.ProductColorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
