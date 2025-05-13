namespace Enterprise.Api.Rest.Extensions;

public static class CommonExtensions
{
    public static async Task<IResult> SendMessageGetResult<T>(this IMediator mediatr, IRequest<T> message)
        where T : OperationResult
    {
        var result = await mediatr.Send(message);
        return result.ToResult<T>();
    }

    public static async Task<IResult> SendMessageGetResult<T>(this IMediator mediatr, IRequest<OperationResult<T>> message)
        where T : class
    {
        var result = await mediatr.Send(message);
        return result.ToResult<T>();
    }

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
