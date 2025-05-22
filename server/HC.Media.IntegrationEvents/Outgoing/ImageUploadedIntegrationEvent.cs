using Enterprise.Events;

namespace HC.Media.IntegrationEvents.Outgoing;

public sealed class ImageUploadedIntegrationEvent(
    Guid RequesterId,
    string ImageUrl
    ) : BaseIntegrationEvent
{
    public Guid RequesterId { get; set; } = RequesterId;
    public string ImageUrl { get; set; } = ImageUrl;
}
