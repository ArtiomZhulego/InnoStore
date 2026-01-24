using System.Transactions;
using Domain.ValueModels;

namespace Domain.Entities;

public sealed class OrderTransaction : BaseEntity
{
    public required Guid OrderId { get; set; }
    public required Guid TransactionId { get; set; }
    
    public required OrderTransactionType TransactionType { get; set; }
    
    public Order? Order { get; set; }
    public Transaction? Transaction { get; set; }
}