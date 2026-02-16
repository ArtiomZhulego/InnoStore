namespace Domain.Exceptions;

public class ProductCategoryNotFoundException(Guid id) : NotFoundException($"Product category with ID {id} was not found");
