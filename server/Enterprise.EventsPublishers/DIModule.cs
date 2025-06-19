using Enterprise.Domain.EventPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.EventsPublishers;

public static class DIModule
{
    public static IServiceCollection AddEnterpriseEventPublishers(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, BaseEventPublisher>();
        return services;
    }
}