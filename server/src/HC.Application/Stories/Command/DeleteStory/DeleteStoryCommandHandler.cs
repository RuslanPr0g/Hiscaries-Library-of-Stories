using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

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