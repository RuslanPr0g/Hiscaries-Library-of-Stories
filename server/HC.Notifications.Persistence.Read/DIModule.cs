using HC.Notifications.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Notifications.Persistence.Read;

public static class DIModule
{
    public static IServiceCollection AddNotificationsPersistenceReadLayer(
        this IServiceCollection services)
    {
        services.AddScoped<INotificationReadRepository, NotificationReadRepository>();
        return services;
    }
}