using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Command.DeleteStory;

public class DeleteStoryCommandHandler : IRequestHandler<DeleteStoryCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public DeleteStoryCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(DeleteStoryCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.DeleteStory(request);
    }
}