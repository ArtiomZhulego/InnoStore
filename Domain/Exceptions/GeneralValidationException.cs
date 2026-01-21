namespace Domain.Exceptions;

public class GeneralValidationException(string message) : BadRequestException(message);
