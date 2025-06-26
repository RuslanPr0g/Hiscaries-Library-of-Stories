using Hiscary.Domain.Images;
using Hiscary.Images.Uploaders;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Media.Images;

public static class DIModule
{
    public static IServiceCollection AddMediaImages(this IServiceCollection services)
    {
        services.AddScoped<IImageUploader, ImageUploader>();
        return services;
    }
}