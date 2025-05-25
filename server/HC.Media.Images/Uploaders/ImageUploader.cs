using Enterprise.Domain.FileStorage;
using Enterprise.Domain.Images;

namespace Enterprise.Images.Uploaders;

public sealed class ImageUploader(
    IImageCompressor imageCompressor,
    IFileStorageService fileStorageService) : IImageUploader
{
    private readonly IImageCompressor _imageCompressor = imageCompressor;
    private readonly IFileStorageService _fileStorageService = fileStorageService;

    public async Task<string> UploadImageAsync(
        Guid fileId,
        string folderPath,
        byte[] imageAsBytes,
        string extension = "jpg",
        CompressionSettings? compressionSettings = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(folderPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(extension);

        if (imageAsBytes.Length == 0)
        {
            throw new ArgumentException($"{nameof(imageAsBytes)} argument cannot be empty.");
        }

        CompressionSettings settings = compressionSettings ?? CompressionSettings.Default;
        byte[] compressedImage = await _imageCompressor.CompressAsync(imageAsBytes, settings);

        var fileName = $"{fileId}.{extension.TrimStart('.')}";
        var imageStoragePath = Path.Combine("images", folderPath);

        return await _fileStorageService.SaveFileAsync(
            fileName,
            imageStoragePath,
            compressedImage);
    }
}
