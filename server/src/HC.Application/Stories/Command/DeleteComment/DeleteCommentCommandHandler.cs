using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

internal class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public DeleteCommentCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.DeleteComment(request);
    }
}