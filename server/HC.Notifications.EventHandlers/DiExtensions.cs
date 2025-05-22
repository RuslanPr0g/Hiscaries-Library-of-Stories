using HC.Notifications.DomainEvents;
using HC.Notifications.EventHandlers.DomainEvents;
using HC.Notifications.EventHandlers.IntegrationEvents;
using HC.Notifications.IntegrationEvents.Incoming;
using HC.PlatformUsers.IntegrationEvents.Outgoing;
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
        services.AddScoped<IConsumer<UserPublishedStoryIntegrationEvent>, UserPublishedStoryIntegrationEventHandler>();
        services.AddScoped<IConsumer<NotificationReferenceObjectIdPreviewChangedIntegrationEvent>,
            NotificationReferenceObjectIdPreviewChangedIntegrationEventHandler>();

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