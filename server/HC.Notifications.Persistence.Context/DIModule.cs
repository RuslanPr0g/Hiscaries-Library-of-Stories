using Enterprise.Persistence.Context.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Notifications.Persistence.Context;

public static class DIModule
{
    public static IServiceCollection AddNotificationsPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddConfigurablePersistenceContext<NotificationsContext>(
            configuration: configuration,
            migrationsAssemblyName: "HC.Notifications.Persistence.Context",
            migrationsHistoryTableSchemaName: NotificationsContext.SCHEMA_NAME);
    }
}