using HC.Application.Interface;
using HC.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Application.Common.Extentions;

public static class ServicesConfiguration
{
    public static IServiceCollection AddServicesServices(this IServiceCollection services)
    {
        services.AddScoped<IUserWriteService, UserService>();
        services.AddScoped<IStoryWriteService, StoryService>();
        services.AddScoped<IStoryPageService, StoryPageService>();
        return services;
    }
}