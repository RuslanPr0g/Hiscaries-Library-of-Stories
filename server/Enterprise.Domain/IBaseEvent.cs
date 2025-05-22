namespace Enterprise.Domain;

public interface IBaseEvent
{
    Guid CorrelationId { get; set; }
    int Version { get; init; }
}
