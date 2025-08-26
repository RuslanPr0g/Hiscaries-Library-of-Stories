namespace Hiscary.PlatformUsers.Api.Rest.Requests.Libraries;

public sealed class EditLibraryRequest
{
    public Guid LibraryId { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public bool ShouldUpdateAvatar { get; set; }
    public List<string> LinksToSocialMedia { get; set; } = [];
}
