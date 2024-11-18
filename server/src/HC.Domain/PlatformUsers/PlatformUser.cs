using HC.Domain.PlatformUsers.Events;
using HC.Domain.Stories;
using HC.Domain.UserAccounts;
using System.Collections.Generic;
using System.Linq;

namespace HC.Domain.PlatformUsers;

/// <summary>
/// Represents a general user of the platform who can perform actions such as 
/// reading, commenting on stories, and optionally creating and managing one or 
/// more libraries. Each published story is linked to a single library owned by 
/// this user.
/// </summary>
public sealed class PlatformUser : AggregateRoot<PlatformUserId>
{
    public PlatformUser(
        PlatformUserId id,
        UserAccountId userId) : base(id)
    {
        UserAccountId = userId;
    }

    public UserAccountId UserAccountId { get; init; }

    public ICollection<Review> Reviews { get; } = [];
    public ICollection<ReadingHistory> ReadHistory { get; } = [];
    public ICollection<StoryBookMark> Bookmarks { get; } = [];

    public bool IsPublisher => Libraries.Count > 0;

    // For now, we allow only one library to be added, but in the future, we can allow to create multiple libraries
    public ICollection<Library> Libraries { get; } = [];

    // TODO: this value should be synced with the user account, as it should be the same
    public string Username { get; set; }

    public void BecomePublisher(LibraryId libraryId)
    {
        if (!IsPublisher)
        {
            Libraries.Add(new Library(libraryId, Id));
        }
    }

    public void ReadStoryPage(StoryId storyId, int page)
    {
        var historyItem = ReadHistory.FirstOrDefault(x => x.StoryId == storyId);

        if (historyItem is not null)
        {
            historyItem.ReadPage(page);
        }
        else
        {
            ReadHistory.Add(new ReadingHistory(Id, storyId, page));
        }

        PublishStoryPageReadEvent(storyId, page);
    }

    public void RemoveReview(LibraryId libraryId)
    {
        if (Reviews.Count > 0)
        {
            var review = Reviews.FirstOrDefault(x => x.LibraryId == libraryId);

            if (review is not null)
            {
                Reviews.Remove(review);
            }
        }
    }

    public void PublishReview(LibraryId libraryId, string? message)
    {
        if (!string.IsNullOrEmpty(message) && Reviews.Count > 0)
        {
            var review = Reviews.FirstOrDefault(x => x.LibraryId == libraryId);

            if (review is null)
            {
                Reviews.Add(
                    new Review(Id, libraryId, message));
            }
            else
            {
                review.Edit(message);
            }
        }
    }

    public void BookmarkStory(StoryBookMarkId id, StoryId storyId)
    {
        if (!Bookmarks.Any(x => x.StoryId == storyId))
        {
            Bookmarks.Add(new StoryBookMark(id, Id, storyId));
        }
    }

    private void PublishStoryPageReadEvent(StoryId storyId, int page)
    {
        PublishEvent(new StoryPageReadDomainEvent(Id, storyId, page));
    }

    private PlatformUser()
    {
    }
}