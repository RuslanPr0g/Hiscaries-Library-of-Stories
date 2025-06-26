using Hiscary.Persistence.Context.Extensions;
using Hiscary.Stories.Persistence.Context.Seeds;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Stories.Persistence.Context;

public static class DIModule
{
    public static IServiceCollection AddStoriesPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        var registredServices = services.AddConfigurablePersistenceContext<StoriesContext>(
            configuration: configuration,
            migrationsAssemblyName: "Hiscary.Stories.Persistence.Context",
            migrationsHistoryTableSchemaName: StoriesContext.SCHEMA_NAME);

        using (var scope = registredServices.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<StoriesContext>();
            GenreSeeder.SeedAsync(db);
        }

        return registredServices;
    }
}