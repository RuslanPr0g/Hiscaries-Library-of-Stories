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

        var hasSlashAtTheEnd = baseUrl[baseUrl.Length - 1] == '/';

        var url = hasSlashAtTheEnd ?
            $"{baseUrl}images/{fileName}" :
            $"{baseUrl}/images/{fileName}";

        return url;
    }
}