namespace Domain.Entities;

public sealed class Transaction : BaseEntity
{
    public Guid Id { get; set; }

    public required TransactionType Type { get; init; }

        public required Guid UserId { get; init; }

    public decimal Amount { get; set; }
}
