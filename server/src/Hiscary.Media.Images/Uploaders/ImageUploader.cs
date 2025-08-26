using Hiscary.Shared.Domain.FileStorage;
using Hiscary.Shared.Domain.Images;

namespace Hiscary.Media.Images.Uploaders;

public sealed class ImageUploader(
    IImageCompressor imageCompressor,
    IBlobStorageService blobStorageService) : IImageUploader
{
    private readonly IImageCompressor _imageCompressor = imageCompressor;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;

    public async Task<string> UploadImageAsync(
        Guid fileId,
        byte[] imageAsBytes,
        string extension = "jpg",
        CompressionSettings? compressionSettings = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(extension);

        if (imageAsBytes is null || imageAsBytes.Length <= 0)
        {
            throw new ArgumentException($"{nameof(imageAsBytes)} argument cannot be empty.");
        }

        CompressionSettings settings = compressionSettings ?? CompressionSettings.Default;
        byte[] compressedImage = await _imageCompressor.CompressAsync(imageAsBytes, settings);

        var fileName = $"{fileId}.{extension.TrimStart('.')}";

        return await _blobStorageService.UploadAsync(
            "images",
            fileName,
            compressedImage,
            cancellationToken: cancellationToken);
    }
}
