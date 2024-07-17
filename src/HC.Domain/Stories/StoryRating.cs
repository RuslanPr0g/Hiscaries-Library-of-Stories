namespace HC.Domain.Stories;

public sealed record class StoryRating
{
    public StoryRating(int id, Story story, User user, int score)
    {
        Id = id;
        Story = story;
        User = user;
        Score = score;
    }

    public int Id { get; init; }
    public Story Story { get; init; }
    public User User { get; init; }
    public int Score { get; init; } // TODO max 5 min 1

    private StoryRating()
    {
    }
}