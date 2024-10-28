using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

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