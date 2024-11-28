using HC.Application.Write.DataAccess;
using HC.Application.Write.Notifications.DataAccess;
using HC.Application.Write.PlatformUsers.DataAccess;
using HC.Application.Write.Stories.DataAccess;
using HC.Application.Write.UserAccounts.DataAccess;
using HC.Persistence.Write.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Persistence.Write;

public static class DiExtensions
{
    public static IServiceCollection AddPersistenceWriteLayer(this IServiceCollection services, IConfiguration connection)
    {
        services.AddScoped<IUserAccountWriteRepository, EFUserAccountWriteRepository>();
        services.AddScoped<IPlatformUserWriteRepository, EFPlatformUserWriteRepository>();
        services.AddScoped<IStoryWriteRepository, EFStoryWriteRepository>();
        services.AddScoped<INotificationWriteRepository, EFNotificationWriteRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}