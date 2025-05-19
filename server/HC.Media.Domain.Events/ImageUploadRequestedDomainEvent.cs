using Enterprise.Domain;

namespace HC.Notifications.Domain.Events;

// TODO: domain event? not sure wtf
public sealed class ImageUploadRequestedDomainEvent(
    byte[] Content,
    Guid RequesterId
    ) : IDomainEvent
{
    public byte[] Content { get; set; } = Content;
    public Guid RequesterId { get; set; } = RequesterId;
}
