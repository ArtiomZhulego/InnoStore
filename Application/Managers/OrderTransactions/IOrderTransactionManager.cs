using Application.Managers.OrderTransactions.Models;
using Domain.Entities;

namespace Application.Managers.OrderTransactions;

internal interface IOrderTransactionManager
{
    public Task<Transaction> AddTransactionToOrderAsync(AddTransactionToOrderModel model, CancellationToken cancellationToken = default);
    public Task<Transaction> RevertOfferTransactionAsync(RevertOrderTransactionModel model, CancellationToken cancellationToken = default);
}