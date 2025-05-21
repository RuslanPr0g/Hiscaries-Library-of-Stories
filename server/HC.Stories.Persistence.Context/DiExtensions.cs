using Enterprise.Persistence.Context;
using HC.Stories.Persistence.Context.Seeds;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Stories.Persistence.Context;

public static class DiExtensions
{
    public static IServiceCollection AddStoriesPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        var registredServices = services.AddBaseEnterprisePersistenceContext<StoriesContext>(
            configuration: configuration,
            migrationsAssemblyName: "HC.Stories.Persistence.Context",
            migrationsHistoryTableSchemaName: StoriesContext.SCHEMA_NAME);

        using (var scope = registredServices.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<StoriesContext>();
            GenreSeeder.SeedAsync(db);
        }

        return registredServices;
    }
}