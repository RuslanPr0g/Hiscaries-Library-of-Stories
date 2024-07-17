using System;
using HC.Domain.Stories;

namespace HC.Domain.Users;

public sealed class UserReadHistory : Entity<UserReadHistoryId>
{
    public UserReadHistory(
        UserReadHistoryId id,
        UserId user,
        StoryId story,
        DateTime dateRead,
        int pageRead,
        bool softDeleted,
        string title,
        string description) : base(id)
    {
        Id = id;
        UserId = user;
        StoryId = story;
        DateLastRead = dateRead;
        LastPageRead = pageRead;
        SoftDeleted = softDeleted;
        Title = title;
        Description = description;
    }

    public UserId UserId { get; init; }
    public StoryId StoryId { get; init; }
    public DateTime DateLastRead { get; init; }
    public int LastPageRead { get; init; }
    public bool SoftDeleted { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }

    private UserReadHistory()
    {
    }
}