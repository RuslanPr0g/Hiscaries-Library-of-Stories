using System;

namespace HC.Domain.Stories.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class StoryPublishedDomainEvent(Guid LibraryId, Guid StoryId, string Title) : IDomainEvent
{
    public Guid LibraryId { get; } = LibraryId;
    public Guid StoryId { get; } = StoryId;
    public string Title { get; } = Title;
}
