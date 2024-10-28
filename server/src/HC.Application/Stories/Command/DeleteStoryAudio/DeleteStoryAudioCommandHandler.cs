using HC.Application.Models.Response;
using HC.Application.Services.Stories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command.DeleteStory;

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