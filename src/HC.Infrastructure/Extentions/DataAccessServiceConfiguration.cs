using HC.Application.Interface;
using HC.Infrastructure.DataAccess;
using HC.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Infrastructure.Extentions;

public static class DataAccessServiceConfiguration
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration connection)
    {
        services.AddScoped<IUserRepository, EFUserRepository>();
        services.AddScoped<IStoryRepository, EFStoryRepository>();

        string mainConnectionString = connection.GetConnectionString("PostgresEF");
        services.AddDbContext<HiscaryContext>(options =>
        {
            options.UseNpgsql(mainConnectionString, b => { b.MigrationsAssembly("HC.Infrastructure"); });
        });

        return services;
    }
}