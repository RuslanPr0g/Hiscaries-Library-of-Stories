using HC.PlatformUsers.Domain.Events;
using HC.PlatformUsers.EventHandlers.DomainEvents;
using HC.PlatformUsers.EventHandlers.IntegrationEvents;
using HC.Stories.Domain.Events;
using HC.UserAccounts.Domain.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HC.PlatformUsers.EventHandlers;

public static class DiExtensions
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IConsumer<StoryPublishedDomainEvent>, StoryPublishedIntegrationEventHandler>();
        services.AddScoped<IConsumer<UserAccountCreatedDomainEvent>, UserAccountCreatedIntegrationEventHandler>();
        services.AddScoped<IConsumer<UserBecamePublisherDomainEvent>, UserBecamePublisherDomainEventHandler>();
        services.AddScoped<IConsumer<UserSubscribedToLibraryDomainEvent>, UserSubscribedToLibraryDomainEventHandler>();
        services.AddScoped<IConsumer<UserUnsubscribedFromLibraryDomainEvent>, UserUnsubscribedFromLibraryDomainEventHandler>();

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