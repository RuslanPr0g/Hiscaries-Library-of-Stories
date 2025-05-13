using HC.Application.Write.ImageCompressors;

namespace HC.Application.Write.ImageUploaderss;

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
        byte[] imagePreview,
        string relativeFolderPath,
        string extension = "jpg",
        CompressionSettings? compressionSettings = null)
    {
        CompressionSettings settings = compressionSettings ?? CompressionSettings.Default;
        byte[] compressedImage = await _imageCompressor.CompressAsync(imagePreview, settings);
        string fileName = $"{relativeFolderPath}/{fileId}.{extension}";
        return await _fileStorageService.SaveFileAsync(compressedImage, fileName);
    }
}
