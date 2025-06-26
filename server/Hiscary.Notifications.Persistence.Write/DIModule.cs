using Hiscary.Notifications.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Notifications.Persistence.Write;

public static class DIModule
{
    public static IServiceCollection AddNotificationsPersistenceWriteLayer(
        this IServiceCollection services)
    {
        services.AddScoped<INotificationWriteRepository, NotificationWriteRepository>();
        return services;
    }
}