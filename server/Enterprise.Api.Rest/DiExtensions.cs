using Enterprise.Application;
using Enterprise.FileStorage;
using Enterprise.Generators;
using Enterprise.Images;
using Enterprise.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Api.Rest;

public static class DiExtensions
{
    public static IServiceCollection AddEnterprise(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEnterpriseApplicationServices(configuration);
        services.AddEnterpriseFileStorage();
        services.AddEnterpriseGenerators();
        services.AddEnterpriseImages();
        services.AddEnterpriseJwt();

        services.AddSingleton<IAuthorizedEndpointHandler, AuthorizedEndpointHandler>();

        return services;
    }
}