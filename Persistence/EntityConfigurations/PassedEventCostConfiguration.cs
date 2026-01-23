using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal class PassedEventCostConfiguration : IEntityTypeConfiguration<PassedEventCost>
{
    public void Configure(EntityTypeBuilder<PassedEventCost> builder)
    {
        builder.ToTable("PassedEventCosts")
            .HasKey(x => x.EventType);

        builder.HasIndex(x => x.EventType)
            .IsUnique();
    }
}
