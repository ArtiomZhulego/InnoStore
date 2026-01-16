namespace Domain.Exceptions;

public class GeneralValidationException : BadRequestException
{
    public GeneralValidationException(string message) : base(message)
    {
    }
}
