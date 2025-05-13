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
    public Guid LibraryId { get; set; }
    public string LibraryName { get; set; }
    public bool IsEditable { get; set; } = false;

    public decimal PercentageRead { get; set; } = 0;
    public int LastPageRead { get; set; }

    public static StorySimpleReadModel FromDomainModel(Story story, decimal percentageRead, int lastPageRead, string? requesterUsername = null)
    {
        // TODO: why use username? why don't use user id?
        return new StorySimpleReadModel
        {
            Id = story.Id,
            Title = story.Title,
            Description = story.Description,
            AuthorName = story.AuthorName,
            AgeLimit = story.AgeLimit,
            DatePublished = story.CreatedAt,
            DateWritten = story.DateWritten,
            LibraryId = story.Library.Id,
            LibraryName = story.Library.PlatformUser.Username,
            IsEditable = story.Library.PlatformUser?.Username == requesterUsername,
            ImagePreviewUrl = story.ImagePreviewUrl,
            PercentageRead = percentageRead,
            LastPageRead = lastPageRead
        };
    }
}