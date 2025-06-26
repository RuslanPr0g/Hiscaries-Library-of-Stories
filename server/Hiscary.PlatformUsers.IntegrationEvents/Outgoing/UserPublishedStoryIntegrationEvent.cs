using Enterprise.Events;

namespace Hiscary.PlatformUsers.IntegrationEvents.Outgoing;

public sealed class UserPublishedStoryIntegrationEvent(
    Guid[] SubscriberIds,
    Guid LibraryId,
    Guid StoryId,
    string Title,
    string? PreviewUrl) : BaseIntegrationEvent
{
    public Guid[] SubscriberIds { get; } = SubscriberIds;
    public Guid LibraryId { get; } = LibraryId;
    public Guid StoryId { get; } = StoryId;
    public string Title { get; } = Title;
    public string? PreviewUrl { get; } = PreviewUrl;
}
