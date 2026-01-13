using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;

namespace Persistence;

public class InnoStoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<ProductSizeLocalization> ProductSizaLocalizations { get; set; }
    public DbSet<ProductGroupLocalization> ProductGroupLocalizations { get; set; }
    public DbSet<ProductLocalization> ProductLocalizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
