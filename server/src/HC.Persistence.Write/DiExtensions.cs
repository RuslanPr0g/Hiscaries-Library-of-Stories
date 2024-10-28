using HC.Application.Write.DataAccess;
using HC.Application.Write.Stories.DataAccess;
using HC.Application.Write.Users.DataAccess;
using HC.Persistence.Write.Repositories;
using HC.Persistence.Write.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Persistence.Write;

public static class DiExtensions
{
    public static IServiceCollection AddPersistenceWriteLayer(this IServiceCollection services, IConfiguration connection)
    {
        services.AddScoped<IUserWriteRepository, EFUserWriteRepository>();
        services.AddScoped<IStoryWriteRepository, EFStoryWriteRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}