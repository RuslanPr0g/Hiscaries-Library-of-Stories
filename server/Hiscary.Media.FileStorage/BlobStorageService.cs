using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Hiscary.Domain.FileStorage;

namespace Hiscary.Media.FileStorage;

public sealed class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        ArgumentNullException.ThrowIfNull(blobServiceClient);
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadAsync(
        string containerName,
        string blobName,
        byte[] data,
        string contentType = "application/octet-stream",
        CancellationToken cancellationToken = default)
    {
        if (data is null || data.Length == 0) throw new ArgumentException("Data cannot be null or empty.", nameof(data));

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

        var blobClient = containerClient.GetBlobClient(blobName);

        await using var stream = new MemoryStream(data);
        await blobClient.UploadAsync(
            stream,
            new BlobHttpHeaders { ContentType = contentType },
            cancellationToken: cancellationToken);

        return blobClient.Uri.ToString();
    }

    public async Task<(Stream content, string contentType)> DownloadAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        if (!await blobClient.ExistsAsync(cancellationToken))
            throw new FileNotFoundException($"Blob '{blobName}' not found in container '{containerName}'.");

        var download = await blobClient.DownloadAsync(cancellationToken);

        var memoryStream = new MemoryStream();
        await download.Value.Content.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;

        return (memoryStream, download.Value.Details.ContentType ?? "application/octet-stream");
    }

    public async Task<bool> ExistsAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        return await blobClient.ExistsAsync(cancellationToken);
    }
}
