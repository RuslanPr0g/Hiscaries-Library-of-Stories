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
    public Story Story { get; }
    public int LastPageRead { get; private set; }
    public bool SoftDeleted { get; init; }

    public decimal CalculatePercentageRead()
    {
        int totalStoryPage = Story.Contents.Count;
        int currentLastPage = LastPageRead;

        return (currentLastPage / totalStoryPage) * 100;
    }

    internal void ReadPage(int page)
    {
        LastPageRead = page;
    }

    private UserReadHistory()
    {
    }
}