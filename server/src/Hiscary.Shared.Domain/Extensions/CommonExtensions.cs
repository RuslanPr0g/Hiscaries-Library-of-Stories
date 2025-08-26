namespace Hiscary.Shared.Domain.Extensions;

public static class CommonExtensions
{
    public static byte[]? GetImageBytes(this string? image)
    {
        if (string.IsNullOrWhiteSpace(image))
        {
            return null;
        }

        int offset = image.IndexOf(',') + 1;
        return Convert.FromBase64String(image[offset..]);
    }
}
