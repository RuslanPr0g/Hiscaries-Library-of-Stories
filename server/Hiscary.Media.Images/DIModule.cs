using StackNucleus.DDD.Domain.Images;
using Microsoft.Extensions.DependencyInjection;
using Hiscary.Media.Images.Uploaders;

namespace Hiscary.Media.Images;

public static class DIModule
{
    public static IServiceCollection AddMediaImages(this IServiceCollection services)
    {
        services.AddScoped<IImageUploader, ImageUploader>();
        return services;
    }
}