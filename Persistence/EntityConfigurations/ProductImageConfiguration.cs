using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasOne(i => i.Product)
               .WithMany(p => p.Images)
               .HasForeignKey(pi => pi.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
