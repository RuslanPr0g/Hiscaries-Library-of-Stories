using Enterprise.Persistence.Context.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.PlatformUsers.Persistence.Context;

public static class DIModule
{
    public static IServiceCollection AddPlatformUsersPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddConfigurablePersistenceContext<PlatformUsersContext>(
            configuration: configuration,
            migrationsAssemblyName: "HC.PlatformUsers.Persistence.Context",
            migrationsHistoryTableSchemaName: PlatformUsersContext.SCHEMA_NAME);
    }
}