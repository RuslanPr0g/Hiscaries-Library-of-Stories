namespace HC.PlatformUsers.Application.Write.PlatformUsers.Command.EditLibrary;

public sealed class EditLibraryCommand : IRequest<OperationResult>
{
    public Guid UserId { get; set; }
    public Guid LibraryId { get; set; }
    public string? Bio { get; set; }
    public byte[]? Avatar { get; set; }
    public bool ShouldUpdateImage { get; set; }
    public List<string> LinksToSocialMedia { get; set; } = [];
}