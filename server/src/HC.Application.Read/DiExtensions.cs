using HC.Application.Read.Notifications.Services;
using HC.Application.Read.Stories.Services;
using HC.Application.Read.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Application.Read;

public static class DIExtensions
{
    public static IServiceCollection AddApplicationReadLayer(this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserReadService, PlatformUserReadService>();
        services.AddScoped<IStoryReadService, StoryReadService>();
        services.AddScoped<INotificationReadService, NotificationReadService>();

        return services;
    }
}