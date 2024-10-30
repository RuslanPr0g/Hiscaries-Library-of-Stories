using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.Users.Command;

public sealed class PublishReviewCommand : IRequest<OperationResult>
{
    public Guid PublisherId { get; set; }
    public Guid ReviewerId { get; set; }
    public string? Message { get; set; }
    public Guid? ReviewId { get; set; }
}