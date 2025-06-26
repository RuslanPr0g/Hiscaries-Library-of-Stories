using Hiscary.Shared.Domain.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Shared.Jwt;

public static class DIModule
{
    public static IServiceCollection AddSharedJwt(this IServiceCollection services)
    {
        services.AddScoped<IJWTTokenHandler, JWTTokenHandler>();
        return services;
    }
}