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

    public async Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId, Guid searchedBy)
    {
        return await _repository.GetStory(storyId, searchedBy);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(Guid userId)
    {
        return await _repository.GetStoryReadingSuggestions(userId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(Guid userId)
    {
        return await _repository.GetStoryResumeReading(userId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(Guid userId)
    {
        return await _repository.GetStoryReadingHistory(userId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> SearchForStory(
        Guid userId,
        Guid? storyId = null,
        Guid? libraryId = null,
        string? searchTerm = null,
        string? genre = null,
        string? requesterUsername = null)
    {
        if (libraryId.HasValue && Guid.Empty != storyId)
        {
            var foundStories = await _repository.GetStorySimpleInfoByLibraryId(libraryId.Value, userId, requesterUsername);
            return foundStories is null ? [] : foundStories;
        }

        if (storyId.HasValue && Guid.Empty != storyId)
        {
            var foundStory = await _repository.GetStorySimpleInfo(storyId.Value, userId, requesterUsername);
            return foundStory is null ? [] : [foundStory];
        }

        if (searchTerm is null)
        {
            return [];
        }

        return await _repository.GetStoriesBy(searchTerm, genre ?? string.Empty, userId);
    }
}