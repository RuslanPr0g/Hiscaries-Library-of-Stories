namespace Enterprise.Domain.Extensions;

public static class CommonExtensions
{
    public static byte[]? GetImageBytes(this string image)
    {
        byte[]? imageInBytes = null;
        if (image is not null)
        {
            int offset = image.IndexOf(',') + 1;
            imageInBytes = Convert.FromBase64String(image[offset..]);
        }
        return imageInBytes;
    }

    public static (byte[] Image, bool IsUpdated) ImageStringToBytes(this string? imageAsString)
    {
        if (string.IsNullOrEmpty(imageAsString))
        {
            return ([], false);
        }

        bool isImageAlreadyUrl = Uri.TryCreate(imageAsString, UriKind.RelativeOrAbsolute, out var _);
        byte[] imageAsBytes = isImageAlreadyUrl ? [] : imageAsString.GetImageBytes() ?? [];
        bool imageWasRemoved = !isImageAlreadyUrl && imageAsBytes.Length == 0;
        bool isImageUpdated = imageAsBytes.Length > 0 || imageWasRemoved;

        return (imageAsBytes, isImageUpdated);
    }
}
