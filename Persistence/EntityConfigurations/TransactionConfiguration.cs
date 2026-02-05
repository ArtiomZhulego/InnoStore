using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions")
            .HasKey(x => x.Id);

        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.HasIndex(x => x.UserId);
    }
}
