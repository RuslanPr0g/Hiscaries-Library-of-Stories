﻿using HC.Application.Read.Notifications.DataAccess;
using HC.Application.Read.Stories.DataAccess;
using HC.Application.Read.Users.DataAccess;
using HC.Persistence.Read.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Persistence.Read;

public static class DiExtensions
{
    public static IServiceCollection AddPersistenceReadLayer(this IServiceCollection services, IConfiguration connection)
    {
        services.AddScoped<IPlatformUserReadRepository, EFPlatformUserReadRepository>();
        services.AddScoped<IStoryReadRepository, EFStoryReadRepository>();
        services.AddScoped<INotificationReadRepository, EFNotificationReadRepository>();

        // TODO: "connection" is it really needed? probably yes, especially if we're going to use dapper for read operations

        return services;
    }
}