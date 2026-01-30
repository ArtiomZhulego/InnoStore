namespace Application.Abstractions.TransactionAggregate;

public sealed record TransactionDTO
{
    public required Guid Id { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required decimal Amount { get; init; }

    public required TransactionDTOType TransactionType { get; init; }
}
