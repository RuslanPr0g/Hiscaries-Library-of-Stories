using System;

namespace HC.Domain.PlatformUsers.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class StoryPageReadDomainEvent(Guid UserId, Guid StoryId, int Page) : IDomainEvent
{
    public Guid UserId { get; } = UserId;
    public Guid StoryId { get; } = StoryId;
    public int Page { get; set; } = Page;
}
