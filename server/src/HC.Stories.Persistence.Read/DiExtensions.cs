using HC.Stories.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Stories.Persistence.Read;

public static class DiExtensions
{
    public static IServiceCollection AddStorysPersistenceReadLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IStoryReadRepository, StoryReadRepository>();
        return services;
    }
}