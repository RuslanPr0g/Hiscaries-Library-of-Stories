namespace Hiscary.PlatformUsers.Api.Rest.Requests.Libraries;

public sealed class UserReadingStoryRequest
{
    public UserReadingStoryItem[] Items { get; set; }
}

public sealed class UserReadingStoryItem
{
    public Guid StoryId { get; set; }
    public Guid LibraryId { get; set; }
    public int TotalPages { get; set; }
}
