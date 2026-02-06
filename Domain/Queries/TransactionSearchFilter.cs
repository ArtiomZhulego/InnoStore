namespace Domain.Queries;

public sealed class TransactionSearchFilter
{
    public required Guid UserId { get; init; }

    public int? PageSize { get; set; }

    public int? PageNumber { get; set; }
}
