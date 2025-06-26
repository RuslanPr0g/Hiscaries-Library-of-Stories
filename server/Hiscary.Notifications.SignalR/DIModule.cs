using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Notifications.SignalR;

public static class DIModule
{
    public static IServiceCollection AddNotificationsSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }
}