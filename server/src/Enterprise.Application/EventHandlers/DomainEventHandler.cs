using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enterprise.Application.EventHandlers;

public abstract class DomainEventHandler<TEvent> : IConsumer<TEvent>
    where TEvent : class
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    protected DomainEventHandler(
        ILogger logger,
        IUnitOfWork unitOfWork
        )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<TEvent> context)
    {
        using (_logger.BeginScope(new { context.CorrelationId }))
        {
            _logger.LogInformation("Starting to process domain event {event}.", typeof(TEvent).Name);
            await HandleEventAsync(context.Message, context);
            _logger.LogInformation("Finished processing domain event {event}.", typeof(TEvent).Name);
        }

        // TODO: not sure if it's okay to leave this here
        await _unitOfWork.SaveChanges();
    }

    protected abstract Task HandleEventAsync(TEvent domainEvent, ConsumeContext<TEvent> context);
}