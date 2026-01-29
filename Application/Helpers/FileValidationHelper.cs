using Application.Constants;
using Domain.Exceptions;

namespace Application.Helpers;

public static class FileValidationHelper
{
    public static void ValidateProductImageExtension(string extension)
    {
        if (FileExtensionConstants.ProductImageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new DocumentExtensionNotMatchException(extension, FileExtensionConstants.ProductImageExtensions);
    }
}
