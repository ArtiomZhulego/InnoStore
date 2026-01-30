using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal sealed class OrderAuditConfiguration : IEntityTypeConfiguration<OrderAudit>
{
    public void Configure(EntityTypeBuilder<OrderAudit> builder)
    {
        builder.ToTable("OrderAudits");

        builder.HasKey(audit => audit.Id);

        builder.Property(audit => audit.CorrelationId)
            .IsRequired();

        builder.Property(audit => audit.ChangedByUserId)
            .IsRequired();

        builder.Property(audit => audit.ActionType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(audit => audit.Data)
            .IsRequired()
            .HasColumnType("jsonb");

        builder.Property(audit => audit.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(audit => audit.UpdatedAt)
             .IsRequired(false);

        builder.HasIndex(audit => audit.OrderId);

        builder.HasIndex(audit => audit.CorrelationId);

        builder.HasOne(audit => audit.Order)
            .WithMany(order => order.Audits)
            .HasForeignKey(audit => audit.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}