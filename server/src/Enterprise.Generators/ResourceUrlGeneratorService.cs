using Microsoft.AspNetCore.Http;

namespace Enterprise.Generators;

public class ResourceUrlGeneratorService(IHttpContextAccessor httpContextAccessor) : IResourceUrlGeneratorService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string GenerateImageUrlByFileName(string fileName)
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request is null) return string.Empty;

        return $"{request.Scheme}://{request.Host}/images/{fileName}";
    }
}