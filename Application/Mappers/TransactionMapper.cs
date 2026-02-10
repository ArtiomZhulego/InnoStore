using Application.Abstractions.Constants;
using Application.Abstractions.TransactionAggregate.Search;
using Domain.Entities;
using Domain.ValueModels;

namespace Application.Mappers;

internal static class TransactionMapper
{
    extension(TransactionSearchFilter filter)
    {
        public Domain.Queries.TransactionSearchFilter ToDomainQuery()
        {
            var pageSize = filter.PageSize;
            var pageNumber = filter.PageNumber;

            if (pageSize is null && pageNumber is null)
            {
                pageSize = TransactionConstants.DefaultSearchPageSize;
                pageNumber = TransactionConstants.DefaultSearchPageNumber;
            }

            return new Domain.Queries.TransactionSearchFilter
            {
                UserId = filter.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
        }
    }

    extension (Transaction transaction)
    {
        public TransactionDTO ToDTO()
        {
            return new TransactionDTO
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt,
                TransactionType = transaction.Type.ToTransactionDTOType(),
            };
        }
    }

    extension(TransactionType transactionType)
    {
        public TransactionDTOType ToTransactionDTOType()
        {
            return transactionType switch
            {
                TransactionType.Unspecified => TransactionDTOType.Unspecified,
                TransactionType.AddForParticipatingInEvent => TransactionDTOType.AddForParticipatingInEvent,
                _ => throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, $"Mapping isn't specified for {nameof(TransactionType)} for value: {transactionType.ToString()}")
            };
        }
    }
}
