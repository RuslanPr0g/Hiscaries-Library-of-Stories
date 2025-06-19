using Enterprise.Domain.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Jwt;

public static class DIModule
{
    public static IServiceCollection AddEnterpriseJwt(this IServiceCollection services)
    {
        services.AddScoped<IJWTTokenHandler, JWTTokenHandler>();
        return services;
    }
}