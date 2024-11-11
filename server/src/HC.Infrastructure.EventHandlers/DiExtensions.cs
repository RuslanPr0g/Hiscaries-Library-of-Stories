using HC.Domain.Users.Events;
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
        services.AddScoped<IConsumer<StoryPageReadDomainEvent>, StoryDomainEventHandler>();

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
