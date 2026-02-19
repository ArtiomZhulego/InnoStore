using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal sealed class ProductQuantityTransactionConfiguration : IEntityTypeConfiguration<ProductQuantityTransaction>
{
    public void Configure(EntityTypeBuilder<ProductQuantityTransaction> builder)
    {
        builder.ToTable("ProductQuantityTransactions");

        builder.HasKey(order => order.Id);

        builder.Property(order => order.OperationAmount)
            .IsRequired();

        builder.Property(order => order.EventType)
            .IsRequired();

        builder.Property(order => order.UserId)
            .IsRequired();

        builder.Property(order => order.ProductSizeId)
            .IsRequired();

        builder.Property(order => order.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(order => order.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(order => order.ProductSize)
            .WithMany(productSize => productSize.ProductQuantityTransactions)
            .HasForeignKey(order => order.ProductSizeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(order => order.User)
            .WithMany(user => user.ProductQuantityTransactions)
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}