using Microsoft.Extensions.DependencyInjection;

namespace HC.Notifications.SignalR;

public static class DiExtensions
{
    public static IServiceCollection AddNotificationsSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }
}