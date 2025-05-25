using Enterprise.Domain.FileStorage;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Media.FileStorage;

public static class DIExtensions
{
    public static IServiceCollection AddMediaFileStorage(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IFileStorageService, LocalFileStorageService>();
        return services;
    }
}