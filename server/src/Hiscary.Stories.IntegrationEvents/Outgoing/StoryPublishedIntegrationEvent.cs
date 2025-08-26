using StackNucleus.DDD.Domain.Events;

namespace Hiscary.Stories.IntegrationEvents.Outgoing;

public sealed class StoryPublishedIntegrationEvent(
    Guid LibraryId,
    Guid StoryId,
    string Title,
    string? PreviewUrl) : BaseIntegrationEvent
{
    public Guid LibraryId { get; } = LibraryId;
    public Guid StoryId { get; } = StoryId;
    public string Title { get; } = Title;
    public string? PreviewUrl { get; } = PreviewUrl;
}
