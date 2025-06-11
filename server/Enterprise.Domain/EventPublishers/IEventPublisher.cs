namespace Enterprise.Domain.EventPublishers;

public interface IEventPublisher
{
    ValueTask Publish<T>(T @event) where T : IBaseEvent;
}
