using HC.Domain.PlatformUsers;
using System;

namespace HC.Domain.Stories;

public sealed class StoryRating : Entity
{
    public StoryRating(
        StoryId storyId,
        PlatformUserId userId,
        int score)
    {
        StoryId = storyId;
        PlatformUserId = userId;

        if (score < 1 || score > 5)
        {
            // TODO: create a domain exception
            // TODO: create a value object for that
            throw new ArgumentException("Provided score was in invalid state.");
        }

        Score = score;
    }

    public StoryId StoryId { get; init; }
    public PlatformUserId PlatformUserId { get; init; }
    public PlatformUser PlatformUser { get; init; }
    public int Score { get; private set; }

    internal void UpdateScore(int score)
    {
        if (score < 1 || score > 5)
        {
            // TODO: create a domain exception
            // TODO: create a value object for that
            throw new ArgumentException("Provided score was in invalid state.");
        }

        Score = score;
    }

    private StoryRating()
    {
    }
}