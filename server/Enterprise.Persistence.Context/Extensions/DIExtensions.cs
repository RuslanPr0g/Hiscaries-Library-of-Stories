using Enterprise.Persistence.Context.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Persistence.Context.Extensions;

public static class DIExtensions
{
    public static IServiceCollection AddConfigurablePersistenceContext<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string migrationsAssemblyName,
        string connectionStringName = "postgres",
        string migrationsHistoryTableSchemaName = "public")
        where TContext : DbContext
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        var mainConnectionString = configuration.GetConnectionString(connectionStringName);
        services.AddDbContext<TContext>((sp, builder) =>
        {
            var intetrceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            builder.UseNpgsql(
                    mainConnectionString,
                    b =>
                    {
                        b.MigrationsAssembly(migrationsAssemblyName)
                            .MigrationsHistoryTable("__EFMigrationsHistory", migrationsHistoryTableSchemaName);
                    })
                .AddInterceptors(intetrceptor!);
        });

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<TContext>();
            db.Database.Migrate();
        }

        return services;
    }
}