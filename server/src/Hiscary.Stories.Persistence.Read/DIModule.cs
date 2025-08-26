using Hiscary.Stories.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Stories.Persistence.Read;

public static class DIModule
{
    public static IServiceCollection AddStoriesPersistenceReadLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IStoryReadRepository, StoryReadRepository>();
        return services;
    }
}