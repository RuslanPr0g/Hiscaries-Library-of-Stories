using Hiscary.PlatformUsers.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.PlatformUsers.Persistence.Write;

public static class DIModule
{
    public static IServiceCollection AddPlatformUsersPersistenceWriteLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserWriteRepository, PlatformUserWriteRepository>();
        return services;
    }
}