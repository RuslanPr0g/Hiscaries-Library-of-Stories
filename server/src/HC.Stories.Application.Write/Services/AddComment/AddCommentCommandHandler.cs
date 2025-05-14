using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Services.AddComment;

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