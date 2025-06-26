using StackNucleus.DDD.Domain.Images;
using Microsoft.Extensions.DependencyInjection;
using Hiscary.Shared.Images.Compressors;

namespace Hiscary.Shared.Images;

public static class DIModule
{
    public static IServiceCollection AddSharedImages(this IServiceCollection services)
    {
        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        return services;
    }
}