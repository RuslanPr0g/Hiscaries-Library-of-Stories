using Enterprise.Domain.FileStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.FileStorage;

public static class DIExtensions
{
    public static IServiceCollection AddEnterpriseFileStorage(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IFileStorageService>(new LocalFileStorageService());

        return services;
    }
}