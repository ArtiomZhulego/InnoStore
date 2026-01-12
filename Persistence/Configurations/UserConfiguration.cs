using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .IsRequired();
        
        builder.Property(u => u.HrmId)
            .IsRequired();

        builder.HasIndex(u => u.HrmId)
            .IsUnique(); 

        builder.Property(u => u.FirstNameRU)
            .HasMaxLength(100)
            .IsUnicode(true);

        builder.Property(u => u.PatronymicRU)
            .HasMaxLength(100)
            .IsUnicode(true);

        builder.Property(u => u.LastNameRU)
            .HasMaxLength(100)
            .IsUnicode(true);

        builder.Property(u => u.FirstNameEN)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(u => u.PatronymicEN)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(u => u.LastNameEN)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.OfficeId);

        builder.Property(u => u.JobTitleId);

        builder.Property(u => u.LinkProfilePictureMini)
            .HasMaxLength(1000);

        builder.Property(u => u.Birthdate)
            .HasColumnType("date");

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("now()");

        builder.Property(u => u.UpdatedAt)
            .HasDefaultValueSql("now()");
    }
}