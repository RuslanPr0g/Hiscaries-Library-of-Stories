namespace HC.Domain.Users;

public sealed class Review : Entity<ReviewId>
{
    public Review(
        ReviewId id,
        User publisher,
        User reviewer,
        string message,
        string username) : base(id)
    {
        Publisher = publisher;
        Reviewer = reviewer;
        Message = message;
        Username = username;
    }

    public User Publisher { get; init; }
    public User Reviewer { get; init; }
    public string Message { get; init; }
    public string Username { get; init; }

    private Review()
    {
    }
}