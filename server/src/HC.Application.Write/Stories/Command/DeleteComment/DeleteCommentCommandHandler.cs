using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Services;
using MediatR;

namespace HC.Application.Write.Stories.Command;

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