using Enterprise.Domain;

namespace HC.PlatformUsers.Domain.Events;

public sealed class UserPublishedStoryDomainEvent(
    Guid[] SubscriberIds,
    Guid LibraryId,
    Guid StoryId,
    string Title,
    string? PreviewUrl) : IDomainEvent
{
    public Guid[] SubscriberIds { get; } = SubscriberIds;
    public Guid LibraryId { get; } = LibraryId;
    public Guid StoryId { get; } = StoryId;
    public string Title { get; } = Title;
    public string? PreviewUrl { get; } = PreviewUrl;
}
