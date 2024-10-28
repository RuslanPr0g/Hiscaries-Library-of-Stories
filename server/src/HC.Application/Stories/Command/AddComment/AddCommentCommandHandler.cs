using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public AddCommentCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.AddComment(request);
    }
}