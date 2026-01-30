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
            OrderId = model.OrderId,
            TransactionId = transaction.Id,
        };
        await orderTransactionsRepository.AddAsync(orderTransaction, cancellationToken);
        
        return transaction;
    }

    public async Task<Transaction> RevertOfferTransactionAsync(RevertOrderTransactionModel model, CancellationToken cancellationToken = default)
    {
        var existingTransactions = await orderTransactionsRepository.GetByOrderId(model.OrderId, cancellationToken);

        decimal totalAmount = 0;
        foreach (var item in existingTransactions)
        {
            if (item.Transaction != null)
                totalAmount += item.Transaction.Amount;
        }

        if (totalAmount <= 0)
        {
            throw new InvalidOperationException($"Order {model.OrderId} has no funds to revert.");
        }

        var refundTransaction = new Transaction
        {
            Id = Guid.NewGuid(),
            UserId = model.UserId,
            Amount = -totalAmount,
            Type = TransactionType.Refund
        };

        await transactionRepository.AddAsync(refundTransaction, cancellationToken);

        var orderTransactionLink = new OrderTransaction
        {
            OrderId = model.OrderId,
            TransactionId = refundTransaction.Id
        };

        await orderTransactionsRepository.AddAsync(orderTransactionLink, cancellationToken);

        return refundTransaction;
    }
}