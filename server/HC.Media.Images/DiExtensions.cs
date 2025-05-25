using Enterprise.Domain.Images;
using Enterprise.Images.Uploaders;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Media.Images;

public static class DIExtensions
{
    public static IServiceCollection AddMediaImages(this IServiceCollection services)
    {
        services.AddScoped<IImageUploader, ImageUploader>();
        return services;
    }
}