namespace Application.Abstractions.UserAggregate;

public sealed record UserBalanceDTO
{
    public required decimal Amount { get; init; }
}
