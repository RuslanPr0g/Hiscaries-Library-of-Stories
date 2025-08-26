using Hiscary.Notifications.Application.Write.Services;
using Hiscary.Notifications.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Notifications.Application.Write;

public static class DIModule
{
    public static IServiceCollection AddNotificationsApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddScoped<INotificationWriteService, NotificationWriteService>();
        return services;
    }
}