using HC.Application.Read.Stories.ReadModels;
using HC.Application.Read.Stories.Services;
using HC.Application.Read.Users.DataAccess;
using MediatR;

namespace HC.Application.Read.Stories.Queries;

public class GetStoryWithContentsHandler : IRequestHandler<GetStoryWithContentsQuery, StoryWithContentsReadModel?>
{
    private readonly IStoryReadService _storySevice;
    private readonly IPlatformUserReadRepository _userRepository;

    public GetStoryWithContentsHandler(IStoryReadService storyService, IPlatformUserReadRepository userService)
    {
        _storySevice = storyService;
        _userRepository = userService;
    }

    public async Task<StoryWithContentsReadModel?> Handle(GetStoryWithContentsQuery request, CancellationToken cancellationToken)
    {
        var platformUserId = await _userRepository.GetPlatformUserIdByUserAccountId(request.UserId);

        if (platformUserId is null)
        {
            return null;
        }

        return await _storySevice.GetStoryById(request.Id, platformUserId);
    }
}