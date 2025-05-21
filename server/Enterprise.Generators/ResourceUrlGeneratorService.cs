using Enterprise.Domain.Generators;

namespace Enterprise.Generators;

public class ResourceUrlGeneratorService : IResourceUrlGeneratorService
{
    public string? GenerateImageUrlByFileName(string baseUrl, string fileName)
    {
        if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(fileName))
        {
            return null;
        }

        var url = $"{baseUrl}/images/{fileName}";

        url = url.Replace("//", "/").Replace("\\", "/").Replace('\\', '/');

        return url;
    }
}