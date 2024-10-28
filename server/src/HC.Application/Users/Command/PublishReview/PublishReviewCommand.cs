using HC.Application.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Users.Command;

public sealed class PublishReviewCommand : IRequest<OperationResult>
{
    public Guid PublisherId { get; set; }
    public Guid ReviewerId { get; set; }
    public string? Message { get; set; }
    public Guid? ReviewId { get; set; }
}