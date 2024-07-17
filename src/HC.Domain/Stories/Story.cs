using HC.Domain.Genres;
using HC.Domain.Stories.Comments;
using HC.Domain.Users;
using System;
using System.Collections.Generic;

namespace HC.Domain.Stories;

public class Story : Entity<StoryId>, IAggregateRoot
{
    private Story(
        StoryId id,
        User publisher,
        string title,
        string description,
        string authorName,
        List<Genre> genres,
        List<Comment> comments,
        List<StoryPage> storyPages,
        int ageLimit,
        byte[] imagePreview,
        DateTime datePublished,
        DateTime dateWritten) : base(id)
    {
        Id = id;
        Publisher = publisher;
        Title = title;
        Description = description;
        AuthorName = authorName;
        Genres = genres;
        Comments = comments;
        StoryPages = storyPages;
        AgeLimit = ageLimit;
        ImagePreview = imagePreview;
        DatePublished = datePublished;
        DateWritten = dateWritten;
    }

    public static Story Create(
        Guid id,
        User publisher,
        string title,
        string description,
        string authorName,
        List<Genre> genres,
        List<Comment> comments,
        List<StoryPage> storyPages,
        int ageLimit,
        byte[] imagePreview,
        DateTime datePublished,
        DateTime dateWritten) => new Story(
            new StoryId(id),
            publisher,
            title,
            description,
            authorName,
            genres,
            comments,
            storyPages,
            ageLimit,
            imagePreview,
            datePublished,
            dateWritten);

    public UserId PublisherId { get; init; }
    public User Publisher { get; init; }
    public List<Genre> Genres { get; init; }
    public List<StoryPage> StoryPages { get; init; }
    public List<Comment> Comments { get; init; }

    public string Title { get; init; }
    public string Description { get; init; }
    public string AuthorName { get; init; }
    public int AgeLimit { get; init; }
    public byte[] ImagePreview { get; private set; }
    public DateTime DatePublished { get; init; }
    public DateTime DateWritten { get; init; }

    public void SetImage(byte[] newImage)
    {
        ImagePreview = newImage;
    }

    protected Story()
    {
    }
}