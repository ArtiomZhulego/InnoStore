namespace Application.Abstractions.TransactionAggregate.Search;

public sealed record TransactionSearchFilter
{
    public required Guid UserId { get; init; }

    public int? PageSize { get; set; }

    public int? PageNumber { get; set; }
}
