using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Services.DeleteComment;

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