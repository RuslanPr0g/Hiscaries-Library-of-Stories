using HC.Notifications.Domain.Events;
using HC.Notifications.EventHandlers.DomainEvents;
using HC.Notifications.EventHandlers.IntegrationEvents;
using HC.PlatformUsers.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HC.Notifications.EventHandlers;

public static class DiExtensions
{
    public static IServiceCollection AddEventHandlers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IConsumer<NotificationCreatedDomainEvent>, NotificationCreatedDomainEventHandler>();
        services.AddScoped<IConsumer<UserPublishedStoryDomainEvent>, UserPublishedStoryIntegrationEventHandler>();

        var rabbitMqConnectionString = configuration.GetConnectionString("rabbitmq");

        services.AddMassTransit(_ =>
        {
            _.SetKebabCaseEndpointNameFormatter();
            _.SetInMemorySagaRepositoryProvider();

            var asm = Assembly.GetExecutingAssembly();

            _.AddConsumers(asm);
            _.AddSagaStateMachines(asm);
            _.AddSagas(asm);
            _.AddActivities(asm);

            _.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitMqConnectionString);
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}