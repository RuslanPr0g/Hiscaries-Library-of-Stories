using HC.Notifications.Domain.Events;
using HC.Notifications.EventHandlers.DomainEvents;
using HC.Notifications.EventHandlers.IntegrationEvents;
using HC.PlatformUsers.Domain.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HC.Notifications.EventHandlers;

public static class DiExtensions
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IConsumer<NotificationCreatedDomainEvent>, NotificationCreatedDomainEventHandler>();
        services.AddScoped<IConsumer<UserPublishedStoryDomainEvent>, UserPublishedStoryIntegrationEventHandler>();

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();

            var asm = Assembly.GetExecutingAssembly();

            x.AddConsumers(asm);
            x.AddSagaStateMachines(asm);
            x.AddSagas(asm);
            x.AddActivities(asm);

            // TODO: use configuration from CI/CD
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}