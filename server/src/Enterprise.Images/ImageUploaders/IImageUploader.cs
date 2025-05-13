using HC.Application.Write.ImageCompressors;

namespace HC.Application.Write.ImageUploaderss;

/// <summary>
/// Defines the contract for uploading images to a storage system with optional compression.
/// </summary>
public interface IImageUploader
{
    /// <summary>
    /// Uploads an image to a specified folder with optional compression applied.
    /// </summary>
    /// <param name="fileId">The unique identifier for the file.</param>
    /// <param name="imageAsBytes">The raw image data as a byte array.</param>
    /// <param name="relativeFolderPath">The relative folder path where the image will be stored.</param>
    /// <param name="extension">The file extension to use for the image (default is "jpg").</param>
    /// <param name="compressionSettings">Optional compression settings to apply before uploading.</param>
    /// <returns>A task that represents the asynchronous upload operation. The result contains the file name or path of the uploaded image.</returns>
    Task<string> UploadImageAsync(
        Guid fileId,
        byte[] imageAsBytes,
        string relativeFolderPath,
        string extension = "jpg",
        CompressionSettings? compressionSettings = null);
}
