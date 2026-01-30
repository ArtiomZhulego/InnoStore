namespace Presentation.Exceptions;

public class PassedEventException : Exception
{
    public PassedEventException(string message, Exception innerException) : base(message, innerException)
    {
    }
}