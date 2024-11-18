using HC.Domain.Stories;

namespace HC.Domain.PlatformUsers;

public sealed class StoryBookMark : Entity<StoryBookMarkId>
{
    public StoryBookMark(
        StoryBookMarkId id,
        PlatformUserId user,
        StoryId story) : base(id)
    {
        Id = id;
        PlatformUserId = user;
        StoryId = story;
    }

    public PlatformUserId PlatformUserId { get; init; }
    public StoryId StoryId { get; init; }

    private StoryBookMark()
    {
    }
}