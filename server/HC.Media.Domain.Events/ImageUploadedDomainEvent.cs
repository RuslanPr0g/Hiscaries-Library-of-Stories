using Enterprise.Domain;

namespace HC.Notifications.Domain.Events;

// TODO: domain event? not sure wtf
public sealed class ImageUploadedDomainEvent(
    Guid RequesterId,
    string ImageUrl
    ) : IDomainEvent
{
    public Guid RequesterId { get; set; } = RequesterId;
    public string ImageUrl { get; set; } = ImageUrl;
}
