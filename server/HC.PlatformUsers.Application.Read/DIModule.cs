using HC.PlatformUsers.Application.Read.Services;
using HC.PlatformUsers.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.PlatformUsers.Application.Read;

public static class DIModule
{
    public static IServiceCollection AddPlatformUsersApplicationReadLayer(this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserReadService, PlatformUserReadService>();
        return services;
    }
}