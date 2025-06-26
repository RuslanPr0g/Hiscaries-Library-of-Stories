using Hiscary.Domain.Images;
using Hiscary.Images.Compressors;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Images;

public static class DIModule
{
    public static IServiceCollection AddSharedImages(this IServiceCollection services)
    {
        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        return services;
    }
}