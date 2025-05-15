using Enterprise.Domain.Images;
using Enterprise.Images.ImageCompressors;
using Enterprise.Images.ImageUploaders;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Images;

public static class DIExtensions
{
    public static IServiceCollection AddEnterpriseImages(this IServiceCollection services)
    {
        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        services.AddScoped<IImageUploader, ImageUploader>();
        return services;
    }
}