namespace Domain.Abstractions;

public interface IStorageService
{
    public Task<string> UploadProductImageAsync(string imageName, string extension, string contentType, Stream fileStream, CancellationToken cancellationToken = default);
    public Task<string> GetQuickAccessUrlAsync(string fileUrl, CancellationToken cancellationToken = default);
    public Task<Stream> GetFileByUrlAsync(string url, CancellationToken cancellationToken = default);
    public Task<bool> ExistsAsync(string url, CancellationToken cancellationToken = default);
    public Task EnsureBucketExistsAsync(CancellationToken cancellationToken = default);
}
