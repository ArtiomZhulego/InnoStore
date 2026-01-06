using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne<ProductGroup>()
               .WithMany(pg => pg.Products)
               .HasForeignKey(p => p.ProductGroupId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
