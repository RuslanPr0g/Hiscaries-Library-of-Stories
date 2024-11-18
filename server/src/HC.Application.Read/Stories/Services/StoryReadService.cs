using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.DataAccess;
using HC.Application.Read.Stories.Queries;
using HC.Application.Read.Stories.ReadModels;

namespace HC.Application.Read.Stories.Services;

public sealed class StoryReadService : IStoryReadService
{
    private readonly IStoryReadRepository _repository;

    public StoryReadService(IStoryReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GenreReadModel>> GetAllGenres() => await _repository.GetAllGenres();

    public async Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId, PlatformUserId searchedBy) => await _repository.GetStory(storyId, searchedBy);

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(GetStoryRecommendationsQuery request)
    {
        return await _repository.GetStoryReadingSuggestions(request.UserId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(GetStoryResumeReadingQuery request)
    {
        return await _repository.GetStoryResumeReading(request.UserId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(GetStoryReadingHistoryQuery request)
    {
        return await _repository.GetStoryReadingHistory(request.UserId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> SearchForStory(GetStoryListQuery request)
    {
        if (request.Id.HasValue && Guid.Empty != request.Id)
        {
            var foundStory = await _repository.GetStorySimpleInfo(request.Id.Value, request.UserId, request.RequesterUsername);
            return foundStory is null ? [] : [foundStory];
        }

        return await _repository.GetStoriesBy(request.SearchTerm, request.Genre, request.UserId);
    }
}