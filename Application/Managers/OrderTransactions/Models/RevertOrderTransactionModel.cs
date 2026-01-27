namespace Application.Managers.OrderTransactions.Models;

internal sealed class RevertOrderTransactionModel
{
    public required Guid OrderId { get; init; }
    public required Guid UserId { get; init; }
}