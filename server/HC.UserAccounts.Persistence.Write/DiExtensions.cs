using HC.UserAccounts.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HC.UserAccounts.Persistence.Write;

public static class DiExtensions
{
    public static IServiceCollection AddUserAccountsPersistenceWriteLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IUserAccountWriteRepository, UserAccountWriteRepository>();
        return services;
    }
}