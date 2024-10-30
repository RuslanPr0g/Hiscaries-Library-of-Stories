using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.Stories.Command;

public class ReadStoryCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid UserId { get; set; }
    public int Page { get; set; }
}