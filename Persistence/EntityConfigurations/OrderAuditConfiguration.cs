using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal sealed class OrderAuditConfiguration : IEntityTypeConfiguration<OrderAudit>
{
    public void Configure(EntityTypeBuilder<OrderAudit> builder)
    {
        builder.HasOne(audit => audit.Order)
            .WithMany(order => order.Audits)
            .HasForeignKey(audit => audit.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}