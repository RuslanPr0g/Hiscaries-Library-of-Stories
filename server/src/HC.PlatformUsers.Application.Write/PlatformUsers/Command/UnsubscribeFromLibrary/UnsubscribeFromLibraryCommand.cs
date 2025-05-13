namespace HC.PlatformUsers.Application.Write.PlatformUsers.Command.UnsubscribeFromLibrary;

public sealed class UnsubscribeFromLibraryCommand : IRequest<OperationResult>
{
    public Guid UserId { get; set; }
    public Guid LibraryId { get; set; }
}