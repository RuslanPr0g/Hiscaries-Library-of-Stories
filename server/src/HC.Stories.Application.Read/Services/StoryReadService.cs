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

    // TODO: we could use a cache for GetPlatformUserIdByUserAccountId, maybe?
    // think whether it is okay, or rethink the approach at all
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
        Guid? id,
        Guid? libraryId,
        string searchTerm,
        string genre,
        string? requesterUsername,
        Guid userId)
    {
        if (libraryId.HasValue && Guid.Empty != id)
        {
            var foundStories = await _repository.GetStorySimpleInfoByLibraryId(libraryId.Value, userId, requesterUsername);
            return foundStories is null ? [] : foundStories;
        }

        if (id.HasValue && Guid.Empty != id)
        {
            var foundStory = await _repository.GetStorySimpleInfo(id.Value, userId, requesterUsername);
            return foundStory is null ? [] : [foundStory];
        }

        return await _repository.GetStoriesBy(searchTerm, genre, userId);
    }
}