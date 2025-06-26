using Hiscary.Shared.Domain.Images;
using Hiscary.Shared.Images.Compressors;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Shared.Images;

public static class DIModule
{
    public static IServiceCollection AddSharedImages(this IServiceCollection services)
    {
        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        return services;
    }
}