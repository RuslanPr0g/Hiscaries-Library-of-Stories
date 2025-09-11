using Hiscary.Shared.Application;
using Hiscary.Shared.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackNucleus.DDD.Api.Rest;
using StackNucleus.DDD.Images;

namespace Hiscary.Shared.Api.Rest;

public static class DIModule
{
    public static IServiceCollection AddSharedRestApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNucleusRestApi();

        services.AddSharedApplicationServices(configuration);
        services.AddNucleusImages();
        services.AddSharedJwt();

        return services;
    }
}