using HC.Application.ResultModels.Response;
using MediatR;

namespace HC.Application.Users.Command;

public sealed class BecomePublisherCommand : IRequest<OperationResult>
{
    public string? Username { get; set; }
}