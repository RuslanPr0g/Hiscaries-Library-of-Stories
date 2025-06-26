using Hiscary.Domain.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Jwt;

public static class DIModule
{
    public static IServiceCollection AddSharedJwt(this IServiceCollection services)
    {
        services.AddScoped<IJWTTokenHandler, JWTTokenHandler>();
        return services;
    }
}