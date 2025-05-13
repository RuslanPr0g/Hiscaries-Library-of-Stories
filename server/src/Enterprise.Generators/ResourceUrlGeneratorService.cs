using Microsoft.AspNetCore.Http;

namespace Enterprise.Generators;

public class ResourceUrlGeneratorService : IResourceUrlGeneratorService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResourceUrlGeneratorService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GenerateImageUrlByFileName(string fileName)
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request is null) return string.Empty;

        return $"{request.Scheme}://{request.Host}/images/{fileName}";
    }
}