namespace HC.Domain;

public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
    where T : Identity
{
    private readonly List<IDomainEvent> _domainEvents;

    protected AggregateRoot(T id) : base(id)
    {
        Id = id;
        _domainEvents = [];
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    public void PublishEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected AggregateRoot()
    {
        _domainEvents = [];
    }
}