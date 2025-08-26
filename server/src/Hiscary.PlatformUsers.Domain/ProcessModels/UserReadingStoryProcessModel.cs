namespace Hiscary.PlatformUsers.Domain.ProcessModels;

public class UserReadingStoryProcessModel
{
    public Guid StoryId { get; set; }
    public Guid LibraryId { get; set; }
    public int TotalPages { get; set; }
}
