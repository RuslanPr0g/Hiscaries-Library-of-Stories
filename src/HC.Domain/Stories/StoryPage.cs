namespace HC.Domain.Stories;

public sealed record class StoryPage
{
    public StoryPage(int id, Story story, int page, string content)
    {
        Id = id;
        Story = story;
        Page = page;
        Content = content;
    }

    public int Id { get; init; }
    public Story Story { get; init; }
    public int Page { get; init; }
    public string Content { get; init; }

    private StoryPage()
    {
    }
}