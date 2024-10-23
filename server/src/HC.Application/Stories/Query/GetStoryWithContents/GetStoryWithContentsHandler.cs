using HC.Application.Interface;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Query;

public class GetStoryWithContentsHandler : IRequestHandler<GetStoryWithContentsQuery, StoryWithContentsReadModel?>
{
    private readonly IStoryReadService _storySevice;

    public GetStoryWithContentsHandler(IStoryReadService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<StoryWithContentsReadModel?> Handle(GetStoryWithContentsQuery request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
        {
            return null;
        }

        return await _storySevice.GetStoryById(request.Id.Value);
    }
}