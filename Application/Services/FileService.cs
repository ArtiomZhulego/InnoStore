using Application.Abstractions.FileAggregate;
using Application.Abstractions.StorageAggregate;
using Application.Helpers;

namespace Application.Services;

public sealed class FileService : IFileService
{
    private readonly IStorageService _storageService;

    public FileService(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<UploadFileResponse> UploadFileAsync(UploadFileModel request, CancellationToken cancellationToken = default)
    {
        var file = request.File;

        var extension = Path.GetExtension(file.FileName);
        FileValidationHelper.ValidateProductImageExtension(extension);

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);

        var fileUrl = await _storageService.UploadProductImageAsync(file.FileName, extension, file.ContentType, memoryStream, cancellationToken);

        return new UploadFileResponse
        {
            FileUrl = fileUrl
        };
    }
}
