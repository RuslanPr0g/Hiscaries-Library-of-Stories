using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.Users.Command;

public sealed class BecomePublisherCommand : IRequest<OperationResult>
{
    public string? Username { get; set; }
}