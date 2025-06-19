using HC.Notifications.Application.Write.Services;
using HC.Notifications.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Notifications.Application.Write;

public static class DIModule
{
    public static IServiceCollection AddNotificationsApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddScoped<INotificationWriteService, NotificationWriteService>();
        return services;
    }
}