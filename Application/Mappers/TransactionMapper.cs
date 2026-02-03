using Application.Managers.OrderTransactions.Models;
using Domain.Entities;

namespace Application.Mappers;

internal static class TransactionMapper
{
    extension(AddTransactionToOrderModel model)
    {
        public Transaction ToTransaction()
        {
            return new Transaction()
            {
                UserId = model.UserId,
                Amount = model.Amount,
                Type = model.TransactionType,
            };
        }
    }
}