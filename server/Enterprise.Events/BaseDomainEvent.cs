using Enterprise.Domain;

namespace Enterprise.Events;

public abstract class BaseDomainEvent : IDomainEvent
{
    public Guid CorrelationId { get; set; }
    public int Version { get; init; } = 1;
}
