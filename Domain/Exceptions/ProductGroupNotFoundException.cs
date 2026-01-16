namespace Domain.Exceptions;

public class ProductGroupNotFoundException : NotFoundException
{
    public ProductGroupNotFoundException(Guid id) : base($"Product group with ID {id} was not found")
    {
    }
}
