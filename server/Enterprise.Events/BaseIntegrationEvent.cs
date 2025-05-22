using Enterprise.Domain;

namespace Enterprise.Events;

public abstract class BaseIntegrationEvent : IIntegrationEvent
{
    public Guid CorrelationId { get; set; }
    public int Version { get; init; } = 1;
}
