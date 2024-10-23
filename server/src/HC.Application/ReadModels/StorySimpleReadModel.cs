using HC.Domain.Stories;
using System;

public class StorySimpleReadModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public int AgeLimit { get; set; }
    public byte[] ImagePreview { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateWritten { get; set; }
    public UserAccountOwnerReadModel Publisher { get; set; }
    public bool IsEditable { get; set; } = false;

    public static StorySimpleReadModel FromDomainModel(Story story, string? requesterUsername = null)
    {
        return new StorySimpleReadModel
        {
            Id = story.Id,
            Title = story.Title,
            Description = story.Description,
            AuthorName = story.AuthorName,
            AgeLimit = story.AgeLimit,
            ImagePreview = story.ImagePreview,
            DatePublished = story.DatePublished,
            DateWritten = story.DateWritten,
            Publisher = UserAccountOwnerReadModel.FromDomainModel(story.Publisher),
            IsEditable = story.Publisher?.Username == requesterUsername,
        };
    }
}