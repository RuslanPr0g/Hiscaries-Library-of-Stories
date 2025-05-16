namespace HC.Stories.Domain.Stories;

public sealed class StoryPage : Entity
{
    public StoryPage(
        StoryId storyId,
        int page,
        string content)
    {
        StoryId = storyId;
        Page = page;
        Content = content;
    }

    public StoryId StoryId { get; init; }
    public int Page { get; init; }

    public string Content { get; init; }

    private StoryPage()
    {
    }
}