namespace Domain.Exceptions;

public sealed class EntityNotFoundException<TEntity>(object id) : NotFoundException($"Enity '{nameof(TEntity)}' with id '{id.ToString()}' not found.");