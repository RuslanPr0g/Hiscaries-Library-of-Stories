namespace HC.Application.Write.ImageCompressors;

/// <summary>
/// Defines the contract for compressing images with customizable settings.
/// </summary>
public interface IImageCompressor
{
    /// <summary>
    /// Compresses an image based on the provided compression settings.
    /// </summary>
    /// <param name="imagePreview">The image data to compress as a byte array.</param>
    /// <param name="settings">The settings to use for compression, such as quality and dimensions.</param>
    /// <returns>A task that represents the asynchronous compression operation. The result contains the compressed image as a byte array.</returns>
    ValueTask<byte[]> CompressAsync(byte[] imagePreview, CompressionSettings settings);
}
