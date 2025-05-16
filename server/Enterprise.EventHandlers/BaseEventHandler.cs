using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enterprise.EventHandlers;

public abstract class BaseEventHandler<TEvent>(
    ILogger logger) : IConsumer<TEvent>
    where TEvent : class
{
    private readonly ILogger _logger = logger;

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        using (_logger.BeginScope(new { context.CorrelationId }))
        {
            _logger.LogInformation("Starting to process domain event {event}.", typeof(TEvent).Name);
            await HandleEventAsync(context.Message, context);
            _logger.LogInformation("Finished processing domain event {event}.", typeof(TEvent).Name);
        }
    }

    protected abstract Task HandleEventAsync(TEvent domainEvent, ConsumeContext<TEvent> context);
}