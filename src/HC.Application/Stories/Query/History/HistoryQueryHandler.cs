using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.Stories.Query.History;

public class HistoryQueryHandler : IRequestHandler<HistoryQuery, List<StoryReadHistoryProgress>>
{
    private readonly IStoryService _storySevice;

    public HistoryQueryHandler(IStoryService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<List<StoryReadHistoryProgress>> Handle(HistoryQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetHistory(request.UserId, request.User);
    }
}