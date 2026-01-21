namespace Domain.Exceptions;

public class ProductGroupNotFoundException(Guid id) : NotFoundException($"Product group with ID {id} was not found");
