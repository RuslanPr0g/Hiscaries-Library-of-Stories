using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.Stories.Query;

public class StoryScoreQueryHandler : IRequestHandler<StoryScoreQuery, List<StoryRating>>
{
    private readonly IStoryService _storySevice;

    public StoryScoreQueryHandler(IStoryService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<List<StoryRating>> Handle(StoryScoreQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetStoryScores(request.User);
    }
}