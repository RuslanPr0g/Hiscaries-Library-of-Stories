using HC.Media.IntegrationEvents.Outgoing;
using HC.PlatformUsers.EventHandlers.IntegrationEvents;
using HC.Stories.IntegrationEvents.Outgoing;
using HC.UserAccounts.IntegrationEvents.Outgoing;
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
        services.AddScoped<IConsumer<StoryPublishedIntegrationEvent>, StoryPublishedIntegrationEventHandler>();
        services.AddScoped<IConsumer<UserAccountCreatedIntegrationEvent>, UserAccountCreatedIntegrationEventHandler>();
        services.AddScoped<IConsumer<ImageUploadedIntegrationEvent>, ImageUploadedIntegrationEventHandler>();

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