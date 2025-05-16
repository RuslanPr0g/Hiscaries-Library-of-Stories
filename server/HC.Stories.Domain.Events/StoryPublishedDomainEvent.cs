using Enterprise.Domain;

namespace HC.Stories.Domain.Events;

public sealed class StoryPublishedDomainEvent(
    Guid LibraryId,
    Guid StoryId,
    string Title,
    string? PreviewUrl) : IDomainEvent
{
    public Guid LibraryId { get; } = LibraryId;
    public Guid StoryId { get; } = StoryId;
    public string Title { get; } = Title;
    public string? PreviewUrl { get; } = PreviewUrl;
}
