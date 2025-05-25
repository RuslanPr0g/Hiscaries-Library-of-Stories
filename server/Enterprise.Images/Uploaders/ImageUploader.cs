using Enterprise.Domain.FileStorage;
using Enterprise.Domain.Images;

namespace Enterprise.Images.Uploaders;

public sealed class ImageUploader : IImageUploader
{
    private readonly IImageCompressor _imageCompressor;
    private readonly IFileStorageService _fileStorageService;

    public ImageUploader(IImageCompressor imageCompressor, IFileStorageService fileStorageService)
    {
        _imageCompressor = imageCompressor;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> UploadImageAsync(
        Guid fileId,
        byte[] imageAsBytes,
        string relativeFolderPath,
        string rootStoragePath,
        string extension = "jpg",
        CompressionSettings? compressionSettings = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(relativeFolderPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootStoragePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(extension);

        if (imageAsBytes.Length == 0)
        {
            throw new ArgumentException($"{nameof(imageAsBytes)} argument cannot be empty.");
        }

        CompressionSettings settings = compressionSettings ?? CompressionSettings.Default;
        byte[] compressedImage = await _imageCompressor.CompressAsync(imageAsBytes, settings);

        var fileName = $"{fileId}.{extension.TrimStart('.')}";
        var imageStoragePath = Path.Combine(rootStoragePath, "images");

        return await _fileStorageService.SaveFileAsync(
            fileName,
            relativeFolderPath,
            imageStoragePath,
            compressedImage);
    }
}
