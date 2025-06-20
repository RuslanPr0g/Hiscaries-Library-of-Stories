﻿using Enterprise.Application;
using Enterprise.EventsPublishers;
using Enterprise.Generators;
using Enterprise.Images;
using Enterprise.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Api.Rest;

public static class DIModule
{
    public static IServiceCollection AddEnterpriseRestApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEnterpriseApplicationServices(configuration);
        services.AddEnterpriseGenerators();
        services.AddEnterpriseImages();
        services.AddEnterpriseJwt();
        services.AddEnterpriseEventPublishers();

        services.AddSingleton<IAuthorizedEndpointHandler, AuthorizedEndpointHandler>();

        return services;
    }
}