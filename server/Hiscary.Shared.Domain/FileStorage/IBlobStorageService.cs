namespace Hiscary.Domain.FileStorage;

public interface IBlobStorageService
{
    Task<string> UploadAsync(
        string containerName,
        string blobName,
        byte[] data,
        string contentType = "application/octet-stream",
        CancellationToken cancellationToken = default);

    Task<(Stream content, string contentType)> DownloadAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default);
}