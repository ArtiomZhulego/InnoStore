namespace Domain.Exceptions;

public class DocumentExtensionNotMatchException(string? extension, params string[]? validExtensions) : BadRequestException($"The file extension '{extension}' is not valid. Allowed extensions: {string.Join(", ", validExtensions ?? [])}")
{
}
