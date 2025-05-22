namespace Enterprise.Domain.EventPublishers;

public interface IEventPublisher
{
    Task Publish<T>(T @event) where T : IBaseEvent;
}
