﻿using Enterprise.Domain;
using HC.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

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
        AgeLimit = ageLimit;
        ImagePreview = imagePreview;
        DatePublished = datePublished;
        DateEdited = datePublished;
        DateWritten = dateWritten;
    }

    public static Story Create(
        StoryId id,
        UserId publisher,
        string title,
        string description,
        string authorName,
        ICollection<Genre> genres,
        int ageLimit,
        byte[] imagePreview,
        DateTime datePublished,
        DateTime dateWritten) => new Story(
            id,
            publisher,
            title,
            description,
            authorName,
            genres,
            ageLimit,
            imagePreview,
            datePublished,
            dateWritten);

    public UserId PublisherId { get; init; }
    public User Publisher { get; init; }
    public ICollection<Genre> Genres { get; init; } = new List<Genre>();
    public List<StoryPage> Contents { get; private set; } = new List<StoryPage>();
    public ICollection<Comment> Comments { get; init; } = new List<Comment>();
    public ICollection<StoryAudio> Audios { get; init; } = new List<StoryAudio>();
    public ICollection<StoryRating> Ratings { get; init; } = new List<StoryRating>();

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string AuthorName { get; private set; }
    public int AgeLimit { get; private set; }
    public byte[] ImagePreview { get; private set; }
    public DateTime DatePublished { get; private set; }
    public DateTime DateEdited { get; private set; }
    public DateTime DateWritten { get; private set; }

    public void AddComment(CommentId commentId, UserId userId, string content, int score, DateTime commentedAt)
    {
        Comments.Add(Comment.Create(commentId, Id, userId, content, commentedAt, score));
    }

    public void SetScoreByUser(UserId userId, int score, StoryRatingId ratingId)
    {
        var existingRating = Ratings.FirstOrDefault(x => x.UserId == userId);

        if (existingRating is not null)
        {
            existingRating.UpdateScore(score);
        }
        else
        {
            Ratings.Add(new StoryRating(ratingId, Id, userId, score));
        }
    }

    public void DeleteComment(CommentId commentId)
    {
        var comment = Comments.FirstOrDefault(x => x.Id == commentId);

        if (comment is not null)
        {
            Comments.Remove(comment);
        }
    }

    public void UpdateComment(CommentId commentId, string content, int score, DateTime updatedAt)
    {
        var comment = Comments.FirstOrDefault(x => x.Id == commentId);

        if (comment is not null)
        {
            comment.UpdateContent(content, score, updatedAt);
        }
    }

    public void UpdateInformation(
        string title,
        string description,
        string authorName,
        ICollection<Genre> genres,
        int ageLimit,
        byte[] imagePreview,
        DateTime dateEdited,
        DateTime dateWritten)
    {
        Title = title;
        Description = description;
        AuthorName = authorName;
        AgeLimit = ageLimit;
        ImagePreview = imagePreview;
        DateEdited = dateEdited;
        DateWritten = dateWritten;

        Genres.Clear();

        foreach (var genre in genres)
        {
            Genres.Add(genre);
        }
    }

    public void ModifyContents(IEnumerable<string> newContents, DateTime editedAt)
    {
        if (Contents is null || Contents.Count == 0)
        {
            Contents = [.. newContents.Select((content, index) => new StoryPage(Id, index, content))];
            DateEdited = editedAt;
            return;
        }

        int currentIndex = 0;

        foreach (var newContent in newContents)
        {
            if (currentIndex < Contents.Count)
            {
                var existingPage = Contents[currentIndex];

                if (existingPage.Content != newContent)
                {
                    Contents[currentIndex] = new StoryPage(Id, currentIndex, newContent);
                }
            }
            else
            {
                Contents.Add(new StoryPage(Id, currentIndex, newContent));
            }

            currentIndex++;
        }

        if (currentIndex < Contents.Count)
        {
            Contents.RemoveRange(currentIndex, Contents.Count - currentIndex);
        }

        DateEdited = editedAt;
    }

    public Guid? ClearAllAudio()
    {
        var fileId = Audios.FirstOrDefault()?.FileId;
        Audios.Clear();
        return fileId;
    }

    public void SetAudio(
        StoryAudioId storyAudioId,
        Guid fileId,
        DateTime updatedAt,
        string name)
    {
        var audio = Audios.FirstOrDefault();

        if (audio is not null)
        {
            audio.UpdateInformation(fileId, name, updatedAt);
        }
        else
        {
            Audios.Add(StoryAudio.Create(storyAudioId, updatedAt, name));
        }
    }

    protected Story()
    {
    }
}