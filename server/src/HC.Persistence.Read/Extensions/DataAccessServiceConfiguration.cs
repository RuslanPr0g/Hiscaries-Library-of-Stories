using HC.Application.Stories.DataAccess;
using HC.Application.Users.DataAccess;
using HC.Persistence.Write.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Persistence.Read.Extensions;

public static class DataAccessServiceConfiguration
{
    public static IServiceCollection AddPersistenceReadLayer(this IServiceCollection services, IConfiguration connection)
    {
        services.AddScoped<IUserReadRepository, EFUserReadRepository>();
        services.AddScoped<IStoryReadRepository, EFStoryReadRepository>();

        // TODO: "connection" is it really needed? probably yes, especially if we're going to use dapper for read operations

        return services;
    }
}