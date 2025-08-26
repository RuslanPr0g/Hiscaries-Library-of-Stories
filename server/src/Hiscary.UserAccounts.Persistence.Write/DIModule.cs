using Hiscary.UserAccounts.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.UserAccounts.Persistence.Write;

public static class DIModule
{
    public static IServiceCollection AddUserAccountsPersistenceWriteLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IUserAccountWriteRepository, UserAccountWriteRepository>();
        return services;
    }
}