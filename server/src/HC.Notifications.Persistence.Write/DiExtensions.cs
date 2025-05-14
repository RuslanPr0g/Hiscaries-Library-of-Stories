using HC.Notifications.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Notifications.Persistence.Write;

public static class DiExtensions
{
    public static IServiceCollection AddNotificationsPersistenceWriteLayer(
        this IServiceCollection services)
    {
        services.AddScoped<INotificationWriteRepository, NotificationWriteRepository>();
        return services;
    }
}