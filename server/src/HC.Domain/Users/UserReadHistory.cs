using HC.Domain.Stories;

namespace HC.Domain.Users;

public sealed class UserReadHistory : Entity
{
    public UserReadHistory(
        UserId user,
        StoryId story,
        int pageRead)
    {
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
        if (page > LastPageRead)
        {
            LastPageRead = page;
        }
    }

    private UserReadHistory()
    {
    }
}