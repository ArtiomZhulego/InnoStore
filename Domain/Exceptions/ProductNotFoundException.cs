namespace Domain.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id) : base($"Product with ID {id} was not found")
    {
    }
}
