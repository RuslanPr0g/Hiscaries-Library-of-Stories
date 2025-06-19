using HC.Notifications.Application.Read.Services;
using HC.Notifications.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Notifications.Application.Read;

public static class DIModule
{
    public static IServiceCollection AddNotificationsApplicationReadLayer(this IServiceCollection services)
    {
        services.AddScoped<INotificationReadService, NotificationReadService>();
        return services;
    }
}