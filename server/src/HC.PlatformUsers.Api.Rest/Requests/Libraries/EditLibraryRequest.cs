namespace HC.API.Requests.Libraries;

public sealed class EditLibraryRequest
{
    public Guid LibraryId { get; set; }
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public List<string> LinksToSocialMedia { get; set; } = [];
}
