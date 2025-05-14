using Enterprise.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Stories.Persistence.Context;

public static class DiExtensions
{
    public static IServiceCollection AddStoriesPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddBaseEnterprisePersistenceContext<StoriesContext>(
            configuration,
            "HC.Stories.Persistence.Context");
    }
}