using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.RabbitMQ;

namespace Enterprise.EventHandlers;

public static class DiExtensions
{
    public static void AddConfigurableEventHandlers(
        this IHostApplicationBuilder builder,
        Assembly[] assemblies,
        string rabbitMqConnectionString,
        string queueName)
    {
        builder.UseWolverine(opts =>
        {
            opts.UseRabbitMq(rabbitMqConnectionString)
                .AutoProvision()
                .ConfigureListeners(
                    listener =>
                        {
                            listener.UseDurableInbox();
                        }
                )
                .DeclareExchange("h-common-exchange",
                    ex =>
                    {
                        ex.BindQueue(queueName, queueName);
                    });

            foreach (var assembly in assemblies)
            {
                opts.Discovery.IncludeAssembly(assembly);
            }

            opts.PublishAllMessages()
                .ToRabbitExchange("h-common-exchange")
                .UseDurableOutbox();

            opts.ListenToRabbitQueue(queueName, cfg =>
            {
                cfg.BindExchange("h-common-exchange", queueName);
            })
            .PreFetchCount(100)
            .ListenerCount(5)
            .UseDurableInbox();

            opts.Policies
                .LogMessageStarting(LogLevel.Information);

            opts.Policies.OnException<Exception>()
                .RetryTimes(3)
                .Then.MoveToErrorQueue();
        });
    }
}