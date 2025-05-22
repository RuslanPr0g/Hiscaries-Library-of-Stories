using Enterprise.Domain;
using Enterprise.Domain.EventPublishers;
using MassTransit;

namespace Enterprise.EventsPublishers;

public class BaseEventPublisher(IPublishEndpoint publisher) : IEventPublisher
{
    private readonly IPublishEndpoint _publisher = publisher;

    public Task Publish<T>(T @event) where T : IBaseEvent
    {
        if (@event.CorrelationId == Guid.Empty)
        {
            @event.CorrelationId = Guid.NewGuid();
        }

        return _publisher.Publish(@event);
    }
}
