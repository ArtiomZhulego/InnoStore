using Application.Managers.OrderTransactions.Models;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueModels;

namespace Application.Managers.OrderTransactions;

internal sealed class OrderTransactionManager(ITransactionRepository transactionRepository,
    IOrderTransactionsRepository orderTransactionsRepository) : IOrderTransactionManager
{
    public async Task<Transaction> AddTransactionToOrderAsync(AddTransactionToOrderModel model, CancellationToken cancellationToken = default)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            UserId = model.UserId,
            Amount = model.Amount,
            Type = model.TransactionType,
        };
        await transactionRepository.AddAsync(transaction, cancellationToken);

        var orderTransaction = new OrderTransaction()
        {
            OrderId = transaction.Id,
            TransactionId = transaction.Id,
            TransactionType = OrderTransactionType.Create,
        };
        await orderTransactionsRepository.AddAsync(orderTransaction, cancellationToken);
        
        return transaction;
    }

    public Task<Transaction> RevertOfferTransactionAsync(RevertOrderTransactionModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}