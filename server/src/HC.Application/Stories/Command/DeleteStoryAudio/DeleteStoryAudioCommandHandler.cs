using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

public class DeleteStoryAudioCommandHandler : IRequestHandler<DeleteStoryAudioCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public DeleteStoryAudioCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(DeleteStoryAudioCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.DeleteAudio(request);
    }
}