using Enterprise.Domain;

namespace Enterprise.EventHandlers;

public interface IDomainEventHandler<TMessage> :
    IEventHandler<TMessage>
    where TMessage : IBaseEvent
{
}
