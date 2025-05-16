using Enterprise.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.PlatformUsers.Persistence.Context;

public static class DiExtensions
{
    public static IServiceCollection AddPlatformUsersPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddBaseEnterprisePersistenceContext<PlatformUsersContext>(
            configuration: configuration,
            migrationsAssemblyName: "HC.PlatformUsers.Persistence.Context",
            migrationsHistoryTableSchemaName: PlatformUsersContext.SCHEMA_NAME);
    }
}