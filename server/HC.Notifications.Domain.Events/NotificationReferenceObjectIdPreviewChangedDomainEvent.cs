using Enterprise.Domain;

namespace HC.Notifications.Domain.Events;

// TODO: not a domain event!? make a clear disctinction pls
public sealed class NotificationReferenceObjectIdPreviewChangedDomainEvent(
    Guid ObjectReferenceId,
    string? PreviewUrl) : IDomainEvent
{
    public Guid ObjectReferenceId { get; set; } = ObjectReferenceId;
    public string? PreviewUrl { get; set; } = PreviewUrl;
}
