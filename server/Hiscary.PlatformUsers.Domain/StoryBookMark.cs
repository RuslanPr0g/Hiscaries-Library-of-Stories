namespace Hiscary.PlatformUsers.Domain;

public sealed class StoryBookMark : Entity
{
    public StoryBookMark(
        PlatformUserId user,
        Guid story)
    {
        PlatformUserId = user;
        StoryId = story;
    }

    public PlatformUserId PlatformUserId { get; init; }
    public Guid StoryId { get; init; }

    private StoryBookMark()
    {
    }
}