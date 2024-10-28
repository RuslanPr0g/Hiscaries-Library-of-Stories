using HC.Application.Models.Response;
using HC.Application.Services.Stories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command.UpdateComment;

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