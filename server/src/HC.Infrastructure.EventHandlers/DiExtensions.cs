using HC.Infrastructure.EventHandlers.DomainEvents.Users;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Infrastructure.EventHandlers;

public static class DiExtensions
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();

            var asm = typeof(UserDomainEventHandler).Assembly;

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
