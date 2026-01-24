using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public sealed class OrderTransactionsConfiguration : IEntityTypeConfiguration<OrderTransaction>
{
    public void Configure(EntityTypeBuilder<OrderTransaction> builder)
    {
        builder.HasKey(orderProduct => new { orderProduct.OrderId, orderProduct.TransactionId });

        builder.HasOne(orderProduct => orderProduct.Order)
            .WithMany(orderProduct => orderProduct.OrderTransactions)
            .HasForeignKey(orderProduct => orderProduct.OrderId);

        builder.HasOne(orderProduct => orderProduct.Transaction)
            .WithMany(transaction => transaction.OrderTransactions)
            .HasForeignKey(orderProduct => orderProduct.TransactionId);
    }
}