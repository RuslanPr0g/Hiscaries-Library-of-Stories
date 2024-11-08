using MassTransit;

namespace HC.Application.Common.EventHandlers;

public interface IDomainEventHandler<TMessage> : 
    IConsumer<TMessage>
    where TMessage : class
{
}
