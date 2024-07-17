using HC.Application.Interface;
using HC.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Application.Common.Extentions;

public static class ServicesConfiguration
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStoryService, StoryService>();
        services.AddScoped<IStoryPageService, StoryPageService>();
        return services;
    }
}