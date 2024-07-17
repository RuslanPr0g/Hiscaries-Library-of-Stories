using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.Stories.Query.Bookmarks;

public class BookmarkStoryQueryHandler : IRequestHandler<BookmarkStoryQuery, List<Story>>
{
    private readonly IStoryService _storySevice;

    public BookmarkStoryQueryHandler(IStoryService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<List<Story>> Handle(BookmarkStoryQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetStoryBookMarksByUserId(request.UserId, request.User);
    }
}