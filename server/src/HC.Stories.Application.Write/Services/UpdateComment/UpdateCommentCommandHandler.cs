using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Services.UpdateComment;

internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public UpdateCommentCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.UpdateComment(request);
    }
}