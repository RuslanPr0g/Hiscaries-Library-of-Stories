using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.PlatformUsers.Command.BecomePublisher;

public sealed class BecomePublisherCommand : IRequest<OperationResult>
{
    public Guid Id { get; set; }
}