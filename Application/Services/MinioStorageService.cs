using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Application.Abstractions.StorageAggregate;
using Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class MinioStorageService : IStorageService
{
    private readonly IAmazonS3 _amazonS3Client;
    private readonly MinioStorageSettings _storageSettings;
    private readonly ILogger<MinioStorageService> _logger;

    public MinioStorageService(ILogger<MinioStorageService> logger, IAmazonS3 amazonS3Client, IOptions<MinioStorageSettings> storageSettings)
    {
        _logger = logger;
        _amazonS3Client = amazonS3Client;
        _storageSettings = storageSettings.Value;
    }

    public async Task<Stream> GetFileByUrlAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        var key = GetFileKey(fileUrl);

        var request = new GetObjectRequest()
        {
            BucketName = _storageSettings.BucketName,
            Key = key
        };

        try
        {
            var response = await _amazonS3Client.GetObjectAsync(request, cancellationToken);
            return response.ResponseStream;
        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogError(ex, "Amazon S3 error occurred.");
            _logger.LogError("Message: {0}", ex.Message);
            _logger.LogError("ErrorCode: {0}", ex.ErrorCode);
            _logger.LogError("StatusCode: {0}", ex.StatusCode);
            _logger.LogError("RequestId: {0}", ex.RequestId);
            _logger.LogError("AmazonId2: {0}", ex.AmazonId2);
            _logger.LogError("Response: {0}", ex.ResponseBody);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown error occurred.");
            throw;
        }
    }

    public Task<string> GetQuickAccessUrlAsync(string fileUrl)
    {
        var key = GetFileKey(fileUrl);
        var request = new GetPreSignedUrlRequest()
        {
            BucketName = _storageSettings.BucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddHours(1),
        };
        return _amazonS3Client.GetPreSignedURLAsync(request);
    }

    public async Task<string> UploadProductImageAsync(string imageName, string extension, string contentType, Stream fileStream, CancellationToken cancellationToken = default)
    {
        var key = $"images/{imageName}-{Guid.NewGuid()}{extension}";
        return await UploadFileAsync(key, fileStream, contentType, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string url, CancellationToken cancellationToken = default)
    {
        if(!url.StartsWith(_storageSettings.SpaceUrl))
        {
            return false;
        }

        var key = GetFileKey(url);

        try
        {
            var request = new GetObjectMetadataRequest
            {
                BucketName = _storageSettings.BucketName,
                Key = key
            };

            await _amazonS3Client.GetObjectMetadataAsync(request, cancellationToken);
            return true;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while checking if file exists: {Url}", url);
            throw;
        }
    }

    public async Task EnsureBucketExistsAsync(CancellationToken cancellationToken = default)
    {
        var bucketName = _storageSettings.BucketName;

        bool bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_amazonS3Client, bucketName);

        if (bucketExists)
        {
            return;
        }

        var putBucketRequest = new PutBucketRequest
        {
            BucketName = bucketName,
            UseClientRegion = true,
            CannedACL = S3CannedACL.Private
        };

        await _amazonS3Client.PutBucketAsync(putBucketRequest, cancellationToken);
        _logger.LogInformation("Bucket '{BucketName}' did not exist and was created successfully.", bucketName);
    }

    private async Task<string> UploadFileAsync(
    string key, Stream fileStream, string? contentType = null, CancellationToken cancellationToken = default)
    {
        if (fileStream.CanSeek)
            fileStream.Seek(0, SeekOrigin.Begin);

        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = fileStream,
            Key = $"{_storageSettings.EnvironmentSubFolder}/{key}",
            BucketName = _storageSettings.BucketName,
            ContentType = contentType,
            CannedACL = S3CannedACL.Private
        };

        using var transferUtility = new TransferUtility(_amazonS3Client);
        await transferUtility.UploadAsync(uploadRequest, cancellationToken);

        await fileStream.DisposeAsync();

        return GetFileUrl(key);
    }

    private string GetFileUrl(string key)
    {
        return $"{_storageSettings.SpaceUrl}/{_storageSettings.BucketName}/{_storageSettings.EnvironmentSubFolder}/{key}";
    }

    private string GetFileKey(string url)
    {
        var baseUrl = $"{_storageSettings.SpaceUrl}/{_storageSettings.BucketName}/";

        int index = url.IndexOf(baseUrl);
        return (index < 0)
                ? url
                : url.Remove(index, baseUrl.Length);
    }
}
