using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence;

public class InnoStoreContext(DbContextOptions options, IEnumerable<IInterceptor>? interceptors) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<ProductCategory> ProductGroups { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<ProductSize> ProductSizes { get; set; }

    public DbSet<ProductSizeLocalization> ProductSizaLocalizations { get; set; }

    public DbSet<ProductCategoryLocalization> ProductGroupLocalizations { get; set; }

    public DbSet<ProductLocalization> ProductLocalizations { get; set; }

    public DbSet<PassedEvent> PassedEvents { get; set; }

    public DbSet<PassedEventCost> PassedEventCosts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<PassedEventParticipant> PassedEventParticipants { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderAudit> OrderAudits { get; set; }

    public DbSet<OrderTransaction> OrderTransactions { get; set; }
    
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
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyMarker).Assembly);
    }
}
