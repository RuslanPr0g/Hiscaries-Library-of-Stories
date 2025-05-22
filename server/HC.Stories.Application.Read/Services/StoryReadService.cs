using HC.Stories.Domain.DataAccess;
using HC.Stories.Domain.ReadModels;

namespace HC.Stories.Application.Read.Services;

public sealed class StoryReadService : IStoryReadService
{
    private readonly IStoryReadRepository _repository;

    public StoryReadService(IStoryReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GenreReadModel>> GetAllGenres() => await _repository.GetAllGenres();

    public async Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId)
    {
        return await _repository.GetStory(storyId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations()
    {
        return await _repository.GetStoryReadingSuggestions();
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading()
    {
        return await _repository.GetStoryResumeReading();
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory()
    {
        return await _repository.GetStoryReadingHistory();
    }

    public async Task<IEnumerable<StorySimpleReadModel>> SearchForStory(
        Guid? storyId = null,
        Guid? libraryId = null,
        string? searchTerm = null,
        string? genre = null)
    {
        if (libraryId.HasValue && Guid.Empty != storyId)
        {
            var foundStories = await _repository.GetStorySimpleInfoByLibraryId(libraryId.Value);
            return foundStories is null ? [] : foundStories;
        }

        if (storyId.HasValue && Guid.Empty != storyId)
        {
            var foundStory = await _repository.GetStorySimpleInfo(storyId.Value);
            return foundStory is null ? [] : [foundStory];
        }

        if (searchTerm is null)
        {
            return [];
        }

        return await _repository.GetStoriesBy(searchTerm, genre ?? string.Empty);
    }
}