using StackNucleus.DDD.Domain.Events;

namespace Hiscary.Media.IntegrationEvents.Incoming;

public sealed class ImageUploadRequestedIntegrationEvent(
    byte[] Content,
    Guid RequesterId,
    string Type
    ) : BaseIntegrationEvent
{
    public byte[] Content { get; set; } = Content;
    public Guid RequesterId { get; set; } = RequesterId;
    public string Type { get; set; } = Type;
}
