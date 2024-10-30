using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Services;
using MediatR;

namespace HC.Application.Write.Stories.Command;

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