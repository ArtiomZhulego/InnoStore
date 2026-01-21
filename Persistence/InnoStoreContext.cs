using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Persistence.EntityConfigurations;

namespace Persistence;

public class InnoStoreContext(DbContextOptions options, IEnumerable<IInterceptor>? interceptors) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<ProductSizeLocalization> ProductSizaLocalizations { get; set; }
    public DbSet<ProductGroupLocalization> ProductGroupLocalizations { get; set; }
    public DbSet<ProductLocalization> ProductLocalizations { get; set; }
    public DbSet<PassedEvent> PassedEvents { get; set; }
    public DbSet<PassedEventParticipant> PassedEventParticipants { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (interceptors is not null && interceptors.Any())
        {
            optionsBuilder.AddInterceptors(interceptors);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PassedEventConfiguration());
        modelBuilder.ApplyConfiguration(new PassedEventParticipantConfiguration());

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
