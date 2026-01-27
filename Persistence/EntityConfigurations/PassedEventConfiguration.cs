using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class PassedEventConfiguration : IEntityTypeConfiguration<PassedEvent>
{
    public void Configure(EntityTypeBuilder<PassedEvent> builder)
    {
        builder.ToTable("PassedEvents")
            .HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.HasMany(x => x.Participants);

        builder.Property(x => x.Name)
            .HasMaxLength(100);
    }
}
