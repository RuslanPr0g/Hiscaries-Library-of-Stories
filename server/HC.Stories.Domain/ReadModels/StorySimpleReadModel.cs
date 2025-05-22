using Enterprise.Domain.ReadModels;
using HC.Stories.Domain.Stories;

namespace HC.Stories.Domain.ReadModels;

public class StorySimpleReadModel : IReadModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public int AgeLimit { get; set; }
    public string? ImagePreviewUrl { get; protected set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateWritten { get; set; }
    public Guid LibraryId { get; set; }
    public int TotalPages { get; set; }

    public static StorySimpleReadModel FromDomainModel(Story story)
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
            LibraryId = story.LibraryId,
            ImagePreviewUrl = story.ImagePreviewUrl,
            TotalPages = story.TotalPages
        };
    }
}