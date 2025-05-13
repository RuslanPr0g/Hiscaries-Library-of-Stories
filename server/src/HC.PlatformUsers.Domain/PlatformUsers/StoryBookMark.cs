using HC.Domain.Stories;

namespace HC.Domain.PlatformUsers;

public sealed class StoryBookMark : Entity
{
    public StoryBookMark(
        PlatformUserId user,
        StoryId story)
    {
        PlatformUserId = user;
        StoryId = story;
    }

    public PlatformUserId PlatformUserId { get; init; }
    public StoryId StoryId { get; init; }

    private StoryBookMark()
    {
    }
}