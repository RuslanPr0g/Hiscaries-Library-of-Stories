using Enterprise.Domain.Images;
using Enterprise.Images.Compressors;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Images;

public static class DIExtensions
{
    public static IServiceCollection AddEnterpriseImages(this IServiceCollection services)
    {
        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        return services;
    }
}