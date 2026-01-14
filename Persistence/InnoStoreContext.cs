using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence;

public class InnoStoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<PassedEvent> PassedEvents { get; set; }

    public DbSet<PassedEventParticipant> PassedEventParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PassedEventConfiguration());
        modelBuilder.ApplyConfiguration(new PassedEventParticipantConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
