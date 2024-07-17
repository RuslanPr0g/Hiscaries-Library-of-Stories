using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Domain.Story.Comment;
using MediatR;

namespace HC.Application.Stories.Query.Comments;

internal class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<Comment>>
{
    private readonly IStoryService _storySevice;

    public GetAllCommentsQueryHandler(IStoryService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<List<Comment>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetAllComments(request.User);
    }
}