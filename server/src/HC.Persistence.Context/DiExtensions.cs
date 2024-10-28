using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Persistence.Context;

public static class DiExtensions
{
    public static IServiceCollection AddPersistenceContext(this IServiceCollection services, IConfiguration connection)
    {
        var mainConnectionString = connection.GetConnectionString("PostgresEF");
        services.AddDbContext<HiscaryContext>(options =>
        {
            options.UseNpgsql(mainConnectionString, b => { b.MigrationsAssembly("HC.Persistence.Context"); });
        });

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<HiscaryContext>();
            db.Database.Migrate();
        }

        return services;
    }
}