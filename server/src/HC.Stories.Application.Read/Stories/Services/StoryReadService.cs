using HC.Stories.Application.Read.Genres.ReadModels;
using HC.Stories.Application.Read.Stories.DataAccess;
using HC.Stories.Application.Read.Stories.Queries.GetStoryList;
using HC.Stories.Application.Read.Stories.Queries.GetStoryReadingHistory;
using HC.Stories.Application.Read.Stories.Queries.GetStoryRecommendations;
using HC.Stories.Application.Read.Stories.Queries.GetStoryResumeReading;
using HC.Stories.Application.Read.Stories.ReadModels;

namespace HC.Stories.Application.Read.Stories.Services;

public sealed class StoryReadService : IStoryReadService
{
    private readonly IStoryReadRepository _repository;
    private readonly IPlatformUserReadRepository _userRepository;

    public StoryReadService(IStoryReadRepository repository, IPlatformUserReadRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<GenreReadModel>> GetAllGenres() => await _repository.GetAllGenres();

    // TODO: we could use a cache for GetPlatformUserIdByUserAccountId, maybe?
    // think whether it is okay, or rethink the approach at all

    public async Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId, Guid searchedBy)
    {
        var platformUserId = await _userRepository.GetPlatformUserIdByUserAccountId(searchedBy);

        if (platformUserId is null)
        {
            return null;
        }

        return await _repository.GetStory(storyId, platformUserId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(GetStoryRecommendationsQuery request)
    {
        var platformUserId = await _userRepository.GetPlatformUserIdByUserAccountId(request.UserId);

        if (platformUserId is null)
        {
            return await _repository.GetLastNStories(6);
        }

        return await _repository.GetStoryReadingSuggestions(platformUserId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(GetStoryResumeReadingQuery request)
    {
        var platformUserId = await _userRepository.GetPlatformUserIdByUserAccountId(request.UserId);

        if (platformUserId is null)
        {
            return [];
        }

        return await _repository.GetStoryResumeReading(platformUserId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(GetStoryReadingHistoryQuery request)
    {
        var platformUserId = await _userRepository.GetPlatformUserIdByUserAccountId(request.UserId);

        if (platformUserId is null)
        {
            return [];
        }

        return await _repository.GetStoryReadingHistory(platformUserId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> SearchForStory(GetStoryListQuery request)
    {
        var platformUserId = await _userRepository.GetPlatformUserIdByUserAccountId(request.UserId);

        if (platformUserId is null)
        {
            return [];
        }

        if (request.LibraryId.HasValue && Guid.Empty != request.Id)
        {
            var foundStories = await _repository.GetStorySimpleInfoByLibraryId(request.LibraryId.Value, platformUserId, request.RequesterUsername);
            return foundStories is null ? [] : foundStories;
        }

        if (request.Id.HasValue && Guid.Empty != request.Id)
        {
            var foundStory = await _repository.GetStorySimpleInfo(request.Id.Value, platformUserId, request.RequesterUsername);
            return foundStory is null ? [] : [foundStory];
        }

        return await _repository.GetStoriesBy(request.SearchTerm, request.Genre, platformUserId);
    }
}