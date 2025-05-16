using Enterprise.Persistence.Context.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Persistence.Context;

public static class DiExtensions
{
    public static IServiceCollection AddBaseEnterprisePersistenceContext<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string migrationsAssemblyName,
        string connectionStringName = "hiscary")
        where TContext : DbContext
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        var mainConnectionString = configuration.GetConnectionString(connectionStringName);
        services.AddDbContext<TContext>((sp, builder) =>
        {
            var intetrceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            builder.UseNpgsql(mainConnectionString, b => { b.MigrationsAssembly(migrationsAssemblyName); })
                .AddInterceptors(intetrceptor!);
        });

        //using (var scope = services.BuildServiceProvider().CreateScope())
        //{
        //    var db = scope.ServiceProvider.GetRequiredService<TContext>();
        //    db.Database.Migrate();
        //}

        return services;
    }
}