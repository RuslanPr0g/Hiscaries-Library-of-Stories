using Enterprise.Domain.Images;
using Enterprise.Images.Uploaders;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Media.Images;

public static class DIModule
{
    public static IServiceCollection AddMediaImages(this IServiceCollection services)
    {
        services.AddScoped<IImageUploader, ImageUploader>();
        return services;
    }
}