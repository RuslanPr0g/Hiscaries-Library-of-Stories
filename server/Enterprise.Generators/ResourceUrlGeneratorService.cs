using Enterprise.Domain.Generators;
using System.Text;

namespace Enterprise.Generators;

public class ResourceUrlGeneratorService : IResourceUrlGeneratorService
{
    public string? GenerateImageUrlByFileName(
        string baseUrl,
        string relativePath,
        string fileName)
    {
        if (
            string.IsNullOrWhiteSpace(baseUrl) ||
            string.IsNullOrWhiteSpace(relativePath) ||
            string.IsNullOrWhiteSpace(fileName))
        {
            return null;
        }

        baseUrl = TrimSlashes(baseUrl);
        relativePath = TrimSlashes(relativePath);
        fileName = TrimSlashes(fileName);

        string fullRelativePath = BuildFullRelativePath("images", relativePath, fileName);

        string resourceUrl = CombineBaseUrlAndRelativePath(baseUrl, fullRelativePath);

        if (!Uri.IsWellFormedUriString(resourceUrl, UriKind.Absolute))
        {
            return null;
        }

        return resourceUrl;

        static string TrimSlashes(string value)
        {
            return value.Trim().TrimEnd().Trim('/').TrimEnd('/');
        }

        static string BuildFullRelativePath(
            string mainFolder,
            string relativeFolder,
            string fileName)
        {
            var pathBuilder = new StringBuilder(mainFolder);
            pathBuilder.Append('/').Append(relativeFolder);
            pathBuilder.Append('/').Append(fileName);
            return pathBuilder.ToString();
        }

        static string CombineBaseUrlAndRelativePath(string baseUrl, string relativePath)
        {
            return $"{baseUrl}/{relativePath}";
        }
    }
}