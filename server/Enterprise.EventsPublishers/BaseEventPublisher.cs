using Enterprise.Domain;
using Enterprise.Domain.EventPublishers;
using Wolverine;

namespace Enterprise.EventsPublishers;

public class BaseEventPublisher(IMessageBus bus) : IEventPublisher
{
    private readonly IMessageBus _bus = bus;

    public ValueTask Publish<T>(T @event) where T : IBaseEvent
    {
        return _bus.PublishAsync(@event);
    }
}
