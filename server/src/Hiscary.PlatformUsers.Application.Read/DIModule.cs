using Hiscary.PlatformUsers.Application.Read.Services;
using Hiscary.PlatformUsers.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.PlatformUsers.Application.Read;

public static class DIModule
{
    public static IServiceCollection AddPlatformUsersApplicationReadLayer(this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserReadService, PlatformUserReadService>();
        return services;
    }
}