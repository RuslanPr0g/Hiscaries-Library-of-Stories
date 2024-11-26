using HC.Domain.Notifications.Events;
using HC.Domain.PlatformUsers.Events;
using HC.Domain.Stories.Events;
using HC.Infrastructure.EventHandlers.DomainEvents.Users;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HC.Infrastructure.EventHandlers;

public static class DiExtensions
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        // TODO: subscribe everything using reflection
        services.AddScoped<IConsumer<StoryPublishedDomainEvent>, StoryPublishedDomainEventHandler>();
        services.AddScoped<IConsumer<UserSubscribedToLibraryDomainEvent>, UserSubscribedToLibraryDomainEventHandler>();
        services.AddScoped<IConsumer<UserUnsubscribedFromLibraryDomainEvent>, UserUnsubscribedFromLibraryDomainEventHandler>();
        services.AddScoped<IConsumer<StoryPublishedDomainEvent>, StoryPublishedDomainEventHandler>();
        services.AddScoped<IConsumer<UserAccountCreatedDomainEvent>, UserAccountCreatedDomainEventHandler>();
        services.AddScoped<IConsumer<UserBecamePublisherDomainEvent>, UserBecamePublisherDomainEventHandler>();

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
