﻿using HC.Stories.Application.Write.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Stories.Application.Write;

public static class DIExtensions
{
    public static IServiceCollection AddStoriesApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddScoped<IStoryWriteService, StoryWriteService>();
        return services;
    }
}