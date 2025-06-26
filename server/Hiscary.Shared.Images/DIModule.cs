using Enterprise.Domain.Images;
using Enterprise.Images.Compressors;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Images;

public static class DIModule
{
    public static IServiceCollection AddEnterpriseImages(this IServiceCollection services)
    {
        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        return services;
    }
}