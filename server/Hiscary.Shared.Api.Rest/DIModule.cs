using Hiscary.Shared.Application;
using Hiscary.Shared.Images;
using Hiscary.Shared.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackNucleus.DDD.Api.Rest;

namespace Hiscary.Shared.Api.Rest;

public static class DIModule
{
    public static IServiceCollection AddSharedRestApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNucleusRestApi();

        services.AddSharedApplicationServices(configuration);
        services.AddSharedImages();
        services.AddSharedJwt();

        return services;
    }
}