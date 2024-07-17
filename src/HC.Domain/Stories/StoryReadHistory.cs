using System;

namespace HC.Domain.Stories;

public sealed record class StoryReadHistory
{
    public StoryReadHistory(int id, User user, Story story, DateTime dateRead, int pageRead, bool softDeleted,
        string title, string description)
    {
        Id = id;
        User = user;
        Story = story;
        DateRead = dateRead;
        PageRead = pageRead;
        SoftDeleted = softDeleted;
        Title = title;
        Description = description;
    }

    public int Id { get; init; }
    public User User { get; init; }
    public Story Story { get; init; }
    public DateTime DateRead { get; init; }
    public int PageRead { get; init; }
    public bool SoftDeleted { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }

    private StoryReadHistory()
    {
    }
}