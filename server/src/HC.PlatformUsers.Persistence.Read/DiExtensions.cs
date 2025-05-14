using HC.PlatformUsers.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HC.PlatformUsers.Persistence.Read;

public static class DiExtensions
{
    public static IServiceCollection AddPlatformUsersPersistenceReadLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserReadRepository, PlatformUserReadRepository>();
        return services;
    }
}