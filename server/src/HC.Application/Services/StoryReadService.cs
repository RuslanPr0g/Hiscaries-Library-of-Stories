using HC.Application.Interface;
using HC.Application.Stories.Query;
using HC.Domain.Stories;
using System.Collections.Generic;
using System.Threading.Tasks;

public sealed class StoryReadService : IStoryReadService
{
    private readonly IStoryReadRepository _repository;

    public StoryReadService(IStoryReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GenreReadModel>> GetAllGenres() => await _repository.GetAllGenres();

    public async Task<StoryWithContentsReadModel?> GetStoryById(StoryId storyId) => await _repository.GetStory(storyId);

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(GetStoryRecommendationsQuery request) =>
        await _repository.GetStoryRecommendations(request.Username);

    public async Task<IEnumerable<StorySimpleReadModel>> SearchForStory(GetStoryListQuery request)
    {
        if (request.Id.HasValue)
        {
            var foundStory = await _repository.GetStorySimpleInfo(request.Id.Value, request.RequesterUsername);
            return foundStory is null ? [] : [foundStory];
        }

        return await _repository.GetStoriesBy(request.SearchTerm, request.Genre);
    }
}