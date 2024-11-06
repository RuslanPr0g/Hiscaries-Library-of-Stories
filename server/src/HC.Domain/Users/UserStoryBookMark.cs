using HC.Domain.Stories;

namespace HC.Domain.Users;

public sealed class UserStoryBookMark : Entity<UserStoryBookMarkId>
{
    public UserStoryBookMark(
        UserStoryBookMarkId id,
        UserId user,
        StoryId story) : base(id)
    {
        Id = id;
        UserId = user;
        StoryId = story;
    }

    public UserId UserId { get; init; }
    public StoryId StoryId { get; init; }

    private UserStoryBookMark()
    {
    }
}