using HC.Stories.Application.Read.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Stories.Application.Read;

public static class DIModule
{
    public static IServiceCollection AddStoriesApplicationReadLayer(this IServiceCollection services)
    {
        services.AddScoped<IStoryReadService, StoryReadService>();
        return services;
    }
}