using HC.PlatformUsers.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HC.PlatformUsers.Persistence.Write;

public static class DiExtensions
{
    public static IServiceCollection AddPlatformUsersPersistenceWriteLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserWriteRepository, PlatformUserWriteRepository>();
        return services;
    }
}