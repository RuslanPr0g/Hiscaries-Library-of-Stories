using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;

using MediatR;

namespace HC.Application.Stories.Command.DeleteComment;

internal class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, int>
{
    private readonly IStoryService _storyService;

    public DeleteCommentCommandHandler(IStoryService storyService)
    {
        _storyService = storyService;
    }

    public async Task<int> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.DeleteComment(new Comment { Id = request.Id }, request.User);
    }
}