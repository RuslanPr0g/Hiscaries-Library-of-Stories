using HC.Notifications.Domain.Events;
using HC.PlatformUsers.EventHandlers.IntegrationEvents;
using HC.Stories.Domain.Events;
using HC.UserAccounts.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HC.PlatformUsers.EventHandlers;

public static class DiExtensions
{
    public static IServiceCollection AddEventHandlers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IConsumer<StoryPublishedDomainEvent>, StoryPublishedIntegrationEventHandler>();
        services.AddScoped<IConsumer<UserAccountCreatedDomainEvent>, UserAccountCreatedIntegrationEventHandler>();
        services.AddScoped<IConsumer<ImageUploadedDomainEvent>, ImageUploadedIntegrationEventHandler>();

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