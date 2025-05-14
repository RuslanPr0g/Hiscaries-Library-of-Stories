using HC.Stories.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Stories.Persistence.Write;

public static class DiExtensions
{
    public static IServiceCollection AddStoriesPersistenceWriteLayer(
        this IServiceCollection services)
    {
        services.AddScoped<IStoryWriteRepository, StoryWriteRepository>();
        services.AddScoped<IGenreWriteRepository, GenreWriteRepository>();
        return services;
    }
}