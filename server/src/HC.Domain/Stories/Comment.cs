using HC.Domain.Users;

namespace HC.Domain.Stories;

public class Comment : Entity<CommentId>
{
    private Comment(
        CommentId id,
        StoryId story,
        UserId user,
        string content,
        int score) : base(id)
    {
        StoryId = story;
        UserId = user;
        Content = content;
        Score = score;
    }

    public static Comment Create(
        CommentId id,
        StoryId story,
        UserId user,
        string content,
        int score) => new(id, story, user, content, score);

    internal void UpdateContent(string content, int score)
    {
        Content = content;
        Score = score;
    }

    public StoryId StoryId { get; init; }
    public Story Story { get; init; }
    public UserId UserId { get; init; }
    public User User { get; init; }

    public string Content { get; private set; }
    public int Score { get; private set; }

    protected Comment()
    {
    }
}