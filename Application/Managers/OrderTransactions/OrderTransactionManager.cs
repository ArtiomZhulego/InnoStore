using Application.Managers.OrderTransactions.Models;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueModels;

namespace Application.Managers.OrderTransactions;

internal sealed class OrderTransactionManager(ITransactionRepository transactionRepository,
    IOrderTransactionsRepository orderTransactionsRepository,
    IDatabaseTransactionManager databaseTransactionManager) : IOrderTransactionManager
{
    public async Task<Transaction> AddTransactionToOrderAsync(AddTransactionToOrderModel model, CancellationToken cancellationToken = default)
    {
        var transaction = model.ToTransaction();

        await databaseTransactionManager.BeginAsync(cancellationToken);
        try
        {
            await transactionRepository.AddAsync(transaction, cancellationToken);

            var orderTransaction = new OrderTransaction()
            {
                OrderId = model.OrderId,
                TransactionId = transaction.Id,
            };
            await orderTransactionsRepository.AddAsync(orderTransaction, cancellationToken);
            
            await databaseTransactionManager.CommitAsync(cancellationToken);
        }
        catch
        {
            await databaseTransactionManager.RollbackAsync(cancellationToken);
            throw;
        }
        
        return transaction;
    }

    public async Task<Transaction> RevertOfferTransactionAsync(RevertOrderTransactionModel model, CancellationToken cancellationToken = default)
    {
        var existingTransactions = await orderTransactionsRepository.GetByOrderId(model.OrderId, cancellationToken);

        decimal totalAmount = 0;
        var items = existingTransactions
            .Where(item => item.Transaction != null)
            .Select(item => item.Transaction!);
        
        foreach (var transaction in items)
        {
            totalAmount += transaction.Amount;
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