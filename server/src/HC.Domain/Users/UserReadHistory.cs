using HC.Domain.Stories;

namespace HC.Domain.Users;

public sealed class UserReadHistory : Entity<UserReadHistoryId>
{
    public UserReadHistory(
        UserReadHistoryId id,
        UserId user,
        StoryId story,
        int pageRead) : base(id)
    {
        Id = id;
        UserId = user;
        StoryId = story;
        LastPageRead = pageRead;

        SoftDeleted = false;
    }

    public UserId UserId { get; init; }
    public StoryId StoryId { get; init; }
    public int LastPageRead { get; private set; }
    public bool SoftDeleted { get; init; }

    internal void ReadPage(int page)
    {
        LastPageRead = page;
    }

    private UserReadHistory()
    {
    }
}