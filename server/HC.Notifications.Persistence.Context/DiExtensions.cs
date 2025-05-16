using Enterprise.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Notifications.Persistence.Context;

public static class DiExtensions
{
    public static IServiceCollection AddNotificationsPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddBaseEnterprisePersistenceContext<NotificationsContext>(
            configuration: configuration,
            migrationsAssemblyName: "HC.Notifications.Persistence.Context",
            migrationsHistoryTableSchemaName: NotificationsContext.SCHEMA_NAME);
    }
}