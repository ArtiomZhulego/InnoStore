namespace Application.Abstractions.TransactionAggregate;

public interface ITransactionService
{
    Task<TransactionDTO[]> GetTransactionsAsync(TransactionSearchFilter filter, CancellationToken cancellationToken);
}