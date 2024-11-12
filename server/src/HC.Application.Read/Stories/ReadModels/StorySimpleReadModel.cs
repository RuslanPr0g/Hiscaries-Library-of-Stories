using HC.Application.Read.Users.ReadModels;
using HC.Domain.Stories;

namespace HC.Application.Read.Stories.ReadModels;

public class StorySimpleReadModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public int AgeLimit { get; set; }
    public string? ImagePreviewUrl { get; protected set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateWritten { get; set; }
    public UserSimpleReadModel Publisher { get; set; }
    public bool IsEditable { get; set; } = false;

    public decimal PercentageRead { get; set; } = 0;

    public static StorySimpleReadModel FromDomainModel(Story story, decimal percentageRead, string? requesterUsername = null)
    {
        return new StorySimpleReadModel
        {
            Id = story.Id,
            Title = story.Title,
            Description = story.Description,
            AuthorName = story.AuthorName,
            AgeLimit = story.AgeLimit,
            DatePublished = story.CreatedAt,
            DateWritten = story.DateWritten,
            Publisher = UserSimpleReadModel.FromDomainModel(story.Publisher),
            IsEditable = story.Publisher?.Username == requesterUsername,
            ImagePreviewUrl = story.ImagePreviewUrl,
            PercentageRead = percentageRead
        };
    }
}