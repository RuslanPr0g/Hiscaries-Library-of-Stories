using Hiscary.Application;
using Hiscary.EventsPublishers;
using Hiscary.Generators;
using Hiscary.Images;
using Hiscary.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Api.Rest;

public static class DIModule
{
    public static IServiceCollection AddEnterpriseRestApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedApplicationServices(configuration);
        services.AddEnterpriseGenerators();
        services.AddSharedImages();
        services.AddSharedJwt();
        services.AddEnterpriseEventPublishers();

        services.AddSingleton<IAuthorizedEndpointHandler, AuthorizedEndpointHandler>();

        return services;
    }
}