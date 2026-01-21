namespace Domain.Exceptions;

public class ProductNotFoundException(Guid id) : NotFoundException($"Product with ID {id} was not found");
