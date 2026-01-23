namespace Application.Abstractions.FileAggregate;

public interface IFileService
{
    public Task<UploadFileResponse> UploadFileAsync(UploadFileModel request, CancellationToken cancellationToken = default);
}
