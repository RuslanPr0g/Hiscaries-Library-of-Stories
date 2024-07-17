using System;

namespace HC.Domain.Stories;

public sealed record class StoryBookMark
{
    public StoryBookMark(int id, User user, Story story, DateTime dateAdded)
    {
        Id = id;
        User = user;
        Story = story;
        DateAdded = dateAdded;
    }

    public int Id { get; init; }
    public User User { get; init; }
    public Story Story { get; init; }
    public DateTime DateAdded { get; init; }

    private StoryBookMark()
    {
    }
}