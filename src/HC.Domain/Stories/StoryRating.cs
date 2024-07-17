using HC.Domain.Users;

namespace HC.Domain.Stories;

public sealed class StoryRating : Entity<StoryRatingId>
{
    public StoryRating(
        StoryRatingId id,
        StoryId story,
        UserId user,
        int score) : base(id)
    {
        Id = id;
        StoryId = story;
        UserId = user;
        Score = score;
    }

    public StoryId StoryId { get; init; }
    public UserId UserId { get; init; }
    public int Score { get; init; } // TODO: max 5 min 1

    private StoryRating()
    {
    }
}