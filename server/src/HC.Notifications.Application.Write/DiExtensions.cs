using Enterprise.Domain.FileStorage;
using Enterprise.Domain.Images;
using Enterprise.Generators;
using Enterprise.Images.ImageUploaders;
using Enterprise.Jwt;
using HC.Notifications.Application.Write.Services;
using HC.Notifications.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Notifications.Application.Write;

public static class DIExtensions
{
    public static IServiceCollection AddNotificationsApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddSingleton<IIdGenerator, IdGenerator>();

        services.AddScoped<IPlatformUserWriteService, PlatformUserWriteService>();
        services.AddScoped<IUserAccountWriteService, UserAccountWriteService>();
        services.AddScoped<IStoryWriteService, StoryWriteService>();
        services.AddScoped<INotificationWriteService, NotificationWriteService>();
        services.AddScoped<IJWTTokenHandler, JWTTokenHandler>();

        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        services.AddScoped<IImageUploader, ImageUploader>();

        services.AddHttpContextAccessor();
        services.AddScoped<IResourceUrlGeneratorService, ResourceUrlGeneratorService>();
        services.AddSingleton<IFileStorageService>(new LocalFileStorageService("wwwroot"));

        return services;
    }
}