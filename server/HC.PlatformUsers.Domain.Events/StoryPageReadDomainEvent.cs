using Enterprise.Domain;

namespace HC.PlatformUsers.Domain.Events;

public sealed class StoryPageReadDomainEvent(
    Guid UserId,
    Guid StoryId,
    int Page) : IDomainEvent
{
    public Guid UserId { get; } = UserId;
    public Guid StoryId { get; } = StoryId;
    public int Page { get; set; } = Page;
}
