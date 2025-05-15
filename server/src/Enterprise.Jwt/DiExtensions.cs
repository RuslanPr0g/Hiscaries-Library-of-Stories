using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Jwt;

public static class DIExtensions
{
    public static IServiceCollection AddEnterpriseJwt(this IServiceCollection services)
    {
        services.AddScoped<IJWTTokenHandler, JWTTokenHandler>();
        return services;
    }
}