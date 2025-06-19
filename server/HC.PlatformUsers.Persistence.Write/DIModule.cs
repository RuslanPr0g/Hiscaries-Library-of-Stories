using HC.PlatformUsers.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HC.PlatformUsers.Persistence.Write;

public static class DIModule
{
    public static IServiceCollection AddPlatformUsersPersistenceWriteLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserWriteRepository, PlatformUserWriteRepository>();
        return services;
    }
}