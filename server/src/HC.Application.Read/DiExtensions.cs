﻿using HC.Application.Read.Stories.Services;
using HC.Application.Read.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Application.Read.Common;

public static class DIExtensions
{
    public static IServiceCollection AddApplicationReadLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserReadService, UserReadService>();
        services.AddScoped<IStoryReadService, StoryReadService>();

        return services;
    }
}