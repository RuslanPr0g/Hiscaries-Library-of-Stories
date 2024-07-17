using Enterprise.Domain;
using HC.Domain.Users;
using System;
using System.Collections.Generic;

namespace HC.Domain.Stories;

public sealed class Story : AggregateRoot<StoryId>
{
    private Story(
        StoryId id,
        UserId publisherId,
        string title,
        string description,
        string authorName,
        ICollection<Genre> genres,
        ICollection<Comment> comments,
        ICollection<StoryPage> storyPages,
        int ageLimit,
        byte[] imagePreview,
        DateTime datePublished,
        DateTime dateWritten) : base(id)
    {
        Id = id;
        PublisherId = publisherId;
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
        UserId publisher,
        string title,
        string description,
        string authorName,
        ICollection<Genre> genres,
        ICollection<Comment> comments,
        ICollection<StoryPage> storyPages,
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
    public ICollection<Genre> Genres { get; init; }
    public ICollection<StoryPage> StoryPages { get; init; }
    public ICollection<Comment> Comments { get; init; }
    public ICollection<StoryAudio> Audios { get; init; }

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