namespace Domain.Exceptions;

public class ImageNotFoundException(string imageUrl) : NotFoundException($" Image with provided url: \"{imageUrl}\" was not found on the bucket")
{
}
