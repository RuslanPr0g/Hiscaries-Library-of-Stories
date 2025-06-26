using Hiscary.PlatformUsers.Application.Write.Services;
using Hiscary.PlatformUsers.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.PlatformUsers.Application.Write;

public static class DIModule
{
    public static IServiceCollection AddPlatformUsersApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserWriteService, PlatformUserWriteService>();
        return services;
    }
}