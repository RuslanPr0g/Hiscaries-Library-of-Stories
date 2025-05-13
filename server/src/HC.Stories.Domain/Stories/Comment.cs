namespace HC.Domain.Stories;

public sealed class Comment : Entity<CommentId>
{
    private Comment(
        CommentId id,
        StoryId storyId,
        PlatformUserId userId,
        string content,
        int score) : base(id)
    {
        StoryId = storyId;
        PlatformUserId = userId;
        Content = content;
        Score = score;
    }

    public static Comment Create(
        CommentId id,
        StoryId storyId,
        PlatformUserId userId,
        string content,
        int score) => new(id, storyId, userId, content, score);

    internal void UpdateContent(string content, int score)
    {
        Content = content;
        Score = score;
    }

    public StoryId StoryId { get; init; }
    public Story Story { get; init; }
    public PlatformUserId PlatformUserId { get; init; }
    public PlatformUser PlatformUser { get; init; }

    public string Content { get; private set; }
    public int Score { get; private set; }

    protected Comment()
    {
    }
}