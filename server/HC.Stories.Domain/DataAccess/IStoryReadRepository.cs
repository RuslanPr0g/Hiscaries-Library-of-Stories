using Enterprise.Domain.DataAccess;
using HC.Stories.Domain.ReadModels;
using HC.Stories.Domain.Stories;

namespace HC.Stories.Domain.DataAccess;

public interface IStoryReadRepository : IBaseReadRepository<StorySimpleReadModel>
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions();
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryWithContentsReadModel?> GetStory(StoryId storyId);
    Task<IEnumerable<StorySimpleReadModel>> GetStorySimpleInfo(
        StoryId[] storyIds,
        string sortProperty = "CreatedAt",
        bool sortAsc = true);
    Task<IEnumerable<StorySimpleReadModel>?> GetStorySimpleInfoByLibraryId(Guid libraryId);
}