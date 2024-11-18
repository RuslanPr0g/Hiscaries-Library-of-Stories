using HC.Domain.Stories;

namespace HC.Domain.PlatformUsers;

public sealed class ReadingHistory : Entity
{
    public ReadingHistory(
        PlatformUserId userId,
        StoryId storyId,
        int pageRead)
    {
        PlatformUserId = userId;
        StoryId = storyId;
        LastPageRead = pageRead;
        SoftDeleted = false;
    }

    public PlatformUserId PlatformUserId { get; init; }
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

    private ReadingHistory()
    {
    }
}