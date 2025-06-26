using Hiscary.Notifications.Application.Read.Services;
using Hiscary.Notifications.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Notifications.Application.Read;

public static class DIModule
{
    public static IServiceCollection AddNotificationsApplicationReadLayer(this IServiceCollection services)
    {
        services.AddScoped<INotificationReadService, NotificationReadService>();
        return services;
    }
}