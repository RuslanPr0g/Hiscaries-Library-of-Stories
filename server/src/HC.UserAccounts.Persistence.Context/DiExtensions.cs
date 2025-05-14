using Enterprise.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.UserAccounts.Persistence.Context;

public static class DiExtensions
{
    public static IServiceCollection AddUserAccountsPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddBaseEnterprisePersistenceContext<UserAccountsContext>(
            configuration,
            "HC.UserAccounts.Persistence.Context");
    }
}