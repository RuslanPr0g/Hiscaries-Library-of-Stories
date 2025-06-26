using Hiscary.Domain.FileStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Media.FileStorage;

public static class DIModule
{
    public static IServiceCollection AddMediaFileStorage(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IBlobStorageService, BlobStorageService>();
        return services;
    }
}