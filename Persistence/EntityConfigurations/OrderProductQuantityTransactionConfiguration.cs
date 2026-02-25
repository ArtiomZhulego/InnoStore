using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfigurations;

internal class OrderProductQuantityTransactionConfiguration : IEntityTypeConfiguration<OrderProductQuantityTransaction>
{
    public void Configure(EntityTypeBuilder<OrderProductQuantityTransaction> builder)
    {
        builder.ToTable("OrderProductQuantityTransactions");

        builder.HasKey(order => order.Id);

        builder.Property(order => order.OrderId)
            .IsRequired();

        builder.Property(order => order.ProductQuantityTransactionId)
            .IsRequired();

        builder.Property(order => order.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(order => order.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(transaction => transaction.Order)
            .WithMany(order => order.OrderProductQuantityTransactions)
            .HasForeignKey(transaction => transaction.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(transaction => transaction.ProductQuantityTransaction)
            .WithOne(productQuantity => productQuantity.OrderProductQuantityTransaction)
            .HasForeignKey<OrderProductQuantityTransaction>(transaction => transaction.ProductQuantityTransactionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}