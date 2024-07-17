namespace HC.Domain.Users;

public sealed record class Review : IAggregateRoot
{
    public Review(int id, User publisher, User reviewer, string message,
        string username)
    {
        Id = id;
        Publisher = publisher;
        Reviewer = reviewer;
        Message = message;
        Username = username;
    }

    public int Id { get; init; }
    public User Publisher { get; init; }
    public User Reviewer { get; init; }
    public string Message { get; init; }
    public string Username { get; init; }

    private Review()
    {
    }
}