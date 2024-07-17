using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Domain.Story.Comment;
using MediatR;

namespace HC.Application.Stories.Command.UpdateComment;

internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, int>
{
    private readonly IStoryService _storyService;

    public UpdateCommentCommandHandler(IStoryService storyService)
    {
        _storyService = storyService;
    }

    public async Task<int> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        List<Comment> comments = await _storyService.GetAllComments(request.User);
        Comment comment = comments.SingleOrDefault(x => x.Id == request.Id);

        if (comment is not null)
        {
            if (request.StoryId > 0)
                comment.StoryId = request.StoryId;
            if (request.UserId > 0)
                comment.UserId = request.UserId;
            if (request.Content != "")
                comment.Content = request.Content;

            return await _storyService.UpdateComment(comment, request.User);
        }

        return -1;
    }
}