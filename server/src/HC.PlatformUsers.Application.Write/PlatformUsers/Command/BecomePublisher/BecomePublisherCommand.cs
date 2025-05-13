namespace HC.PlatformUsers.Application.Write.PlatformUsers.Command.BecomePublisher;

public sealed class BecomePublisherCommand : IRequest<OperationResult>
{
    public Guid Id { get; set; }
}