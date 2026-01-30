using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class PassedEventParticipantConfiguration : IEntityTypeConfiguration<PassedEventParticipant>
{
    public void Configure(EntityTypeBuilder<PassedEventParticipant> builder)
    {
        builder.ToTable("PassedEventParticipants")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(x => x.HrmId);

        builder.HasOne<PassedEvent>()
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.PassedEventId)
            .IsRequired();
    }
}
