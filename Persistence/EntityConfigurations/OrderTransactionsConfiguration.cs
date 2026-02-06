using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public sealed class OrderTransactionsConfiguration : IEntityTypeConfiguration<OrderTransaction>
{
    public void Configure(EntityTypeBuilder<OrderTransaction> builder)
    {
        builder.ToTable("OrderTransactions");

        builder.Property(orderTransaction => orderTransaction.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(orderTransaction => orderTransaction.UpdatedAt)
             .IsRequired(false);

        builder.HasKey(orderTransaction => new { orderTransaction.OrderId, orderTransaction.TransactionId });

        builder.HasOne(orderTransaction => orderTransaction.Order)
            .WithMany(order => order.OrderTransactions)
            .HasForeignKey(orderTransaction => orderTransaction.OrderId);

        builder.HasOne(orderTransaction => orderTransaction.Transaction)
            .WithMany(transaction => transaction.OrderTransactions)
            .HasForeignKey(orderTransaction => orderTransaction.TransactionId);
    }
}