using Enterprise.Domain;

public interface IEventHandler<TEvent> where TEvent : IBaseEvent
{
}