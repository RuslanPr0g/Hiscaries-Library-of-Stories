using HC.Application.Models.Response;
using MediatR;
using System;

namespace HC.Application.Users.Command;

public sealed class DeleteReviewCommand : IRequest<OperationResult>
{
    public string Username { get; set; }
    public Guid ReviewId { get; set; }
}