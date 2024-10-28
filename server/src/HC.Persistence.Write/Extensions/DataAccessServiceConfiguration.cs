using HC.Application.DataAccess;
using HC.Application.Stories.DataAccess;
using HC.Application.Users.DataAccess;
using HC.Persistence.Write.DataAccess;
using HC.Persistence.Write.Repositories;
using HC.Persistence.Write.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Persistence.Write.Extensions;

public static class DataAccessServiceConfiguration
{
    public static IServiceCollection AddPersistenceWriteLayer(this IServiceCollection services, IConfiguration connection)
    {
        services.AddScoped<IUserWriteRepository, EFUserWriteRepository>();
        services.AddScoped<IStoryWriteRepository, EFStoryWriteRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        var mainConnectionString = connection.GetConnectionString("PostgresEF");
        services.AddDbContext<HiscaryContext>(options =>
        {
            options.UseNpgsql(mainConnectionString, b => { b.MigrationsAssembly("HC.Persistence.Write"); });
        });

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<HiscaryContext>();
            db.Database.Migrate();
        }

        return services;
    }
}