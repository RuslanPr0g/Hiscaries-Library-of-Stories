using Enterprise.Persistence.Context;
using Enterprise.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Persistence;

public static class DiExtensions
{
    public static IServiceCollection AddEnterprisePersistenceContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        var mainConnectionString = configuration.GetConnectionString("PostgresEF");
        services.AddDbContext<EnterpriseContext>((sp, builder) =>
        {
            var intetrceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            builder.UseNpgsql(mainConnectionString, b => { b.MigrationsAssembly("Enterprise.Persistence.Context"); })
                .AddInterceptors(intetrceptor!);
        });

        // TODO: this is a library, we need to disable it for regular devs
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<EnterpriseContext>();
            db.Database.Migrate();
        }

        return services;
    }
}