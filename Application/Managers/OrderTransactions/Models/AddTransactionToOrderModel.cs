using Domain.ValueModels;

namespace Application.Managers.OrderTransactions.Models;

internal sealed class AddTransactionToOrderModel
{
    public required Guid OrderId { get; init; }
    public required Guid UserId { get; init; }
    public required decimal Amount { get; init; }
    public required TransactionType TransactionType { get; init; }
}