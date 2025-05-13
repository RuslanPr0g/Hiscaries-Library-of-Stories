using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Persistence.Context;

public static class DiExtensions
{
    public static IServiceCollection AddPersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        var mainConnectionString = configuration.GetConnectionString("PostgresEF");
        services.AddDbContext<HiscaryContext>((sp, builder) =>
        {
            var intetrceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            builder.UseNpgsql(mainConnectionString, b => { b.MigrationsAssembly("HC.Persistence.Context"); })
                .AddInterceptors(intetrceptor!);
        });

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<HiscaryContext>();
            db.Database.Migrate();
        }

        return services;
    }
}