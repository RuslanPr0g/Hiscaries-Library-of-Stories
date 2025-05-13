using MassTransit;

namespace Enterprise.Application.EventHandlers;

public interface IDomainEventHandler<TMessage> :
    IConsumer<TMessage>
    where TMessage : class
{
}
