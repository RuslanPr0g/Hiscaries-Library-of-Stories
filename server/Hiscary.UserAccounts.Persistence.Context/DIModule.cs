using Enterprise.Persistence.Context.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.UserAccounts.Persistence.Context;

public static class DIModule
{
    public static IServiceCollection AddUserAccountsPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddConfigurablePersistenceContext<UserAccountsContext>(
            configuration: configuration,
            migrationsAssemblyName: "Hiscary.UserAccounts.Persistence.Context",
            migrationsHistoryTableSchemaName: UserAccountsContext.SCHEMA_NAME);
    }
}