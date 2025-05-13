namespace HC.PlatformUsers.Application.Write.PlatformUsers.Command.SubscribeToLibrary;

public sealed class SubscribeToLibraryCommand : IRequest<OperationResult>
{
    public Guid UserId { get; set; }
    public Guid LibraryId { get; set; }
}