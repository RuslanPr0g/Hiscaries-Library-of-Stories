using MassTransit;

namespace Enterprise.EventHandlers;

public interface IDomainEventHandler<TMessage> :
    IConsumer<TMessage>
    where TMessage : class
{
}
