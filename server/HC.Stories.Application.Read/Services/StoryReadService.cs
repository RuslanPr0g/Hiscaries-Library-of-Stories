using HC.Stories.Domain.DataAccess;
using HC.Stories.Domain.ReadModels;
using HC.Stories.Domain.Stories;

namespace HC.Stories.Application.Read.Services;

public sealed class StoryReadService : IStoryReadService
{
    private readonly IStoryReadRepository _repository;

    public StoryReadService(IStoryReadRepository repository)
    {
        _repository = repository;
    }

    public HashSet<string> SortableProperties => new(StringComparer.OrdinalIgnoreCase)
    {
        "Title",
        "CreatedAt",
        "DateWritten",
    };

    public async Task<IEnumerable<GenreReadModel>> GetAllGenres() => await _repository.GetAllGenres();

    public async Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId)
    {
        return await _repository.GetStory(storyId);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryByIds(
        StoryId[] storyIds,
        string sortProperty = "CreatedAt",
        bool sortAsc = true)
    {
        if (!SortableProperties.Contains(sortProperty))
        {
            sortProperty = "CreatedAt";
        }

        return await _repository.GetStorySimpleInfo(storyIds, sortProperty, sortAsc);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations()
    {
        return await _repository.GetStoryReadingSuggestions();
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
            var foundStories = await _repository.GetStorySimpleInfo([storyId.Value]);
            var foundStory = foundStories.FirstOrDefault();
            return foundStory is null ? [] : [foundStory];
        }

        if (searchTerm is null)
        {
            return [];
        }

        return await _repository.GetStoriesBy(searchTerm, genre ?? string.Empty);
    }
}