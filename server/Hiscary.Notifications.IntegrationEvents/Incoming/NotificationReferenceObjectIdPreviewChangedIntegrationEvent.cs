using Enterprise.Events;

namespace Hiscary.Notifications.IntegrationEvents.Incoming;

public sealed class NotificationReferenceObjectIdPreviewChangedIntegrationEvent(
    Guid ObjectReferenceId,
    string? PreviewUrl) : BaseIntegrationEvent
{
    public Guid ObjectReferenceId { get; set; } = ObjectReferenceId;
    public string? PreviewUrl { get; set; } = PreviewUrl;
}
