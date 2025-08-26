using Hiscary.PlatformUsers.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.PlatformUsers.Persistence.Read;

public static class DIModule
{
    public static IServiceCollection AddPlatformUsersPersistenceReadLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserReadRepository, PlatformUserReadRepository>();
        return services;
    }
}