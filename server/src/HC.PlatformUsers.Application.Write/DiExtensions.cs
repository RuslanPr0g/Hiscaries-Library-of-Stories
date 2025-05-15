using HC.PlatformUsers.Application.Write.Services;
using HC.PlatformUsers.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.PlatformUsers.Application.Write;

public static class DIExtensions
{
    public static IServiceCollection AddPlatformUsersApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddScoped<IPlatformUserWriteService, PlatformUserWriteService>();
        return services;
    }
}