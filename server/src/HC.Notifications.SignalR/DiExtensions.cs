using Microsoft.Extensions.DependencyInjection;

namespace HC.Infrastructure.Jobs;

public static class DiExtensions
{
    public static IServiceCollection AddHiscariesSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }
}