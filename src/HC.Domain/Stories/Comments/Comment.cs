using HC.Domain.Users;
using System;

namespace HC.Domain.Stories.Comments;

public class Comment : Entity<CommentId>
{
    private Comment(
        CommentId id,
        Story story,
        User user,
        string content,
        DateTime commentedAt,
        string username,
        int score) : base(id)
    {
        StoryId = story.Id;
        Story = story;
        UserId = user.Id;
        User = user;
        Content = content;
        CommentedAt = commentedAt;
        Username = username;
        Score = score;
    }

    public static Comment Create(
        Guid id,
        Story story,
        User user,
        string content,
        DateTime commentedAt,
        string username,
        int score) => new Comment(new CommentId(id), story, user, content, commentedAt, username, score);

    public StoryId StoryId { get; init; }
    public Story Story { get; init; }
    public UserId UserId { get; init; }
    public User User { get; init; }

    public string Content { get; init; }
    public DateTime CommentedAt { get; init; }
    public string Username { get; init; }
    public int Score { get; init; }

    protected Comment()
    {
    }
}