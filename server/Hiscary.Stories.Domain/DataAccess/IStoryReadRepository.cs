using Hiscary.Domain.DataAccess;
using Hiscary.Stories.Domain.ReadModels;
using Hiscary.Stories.Domain.Stories;

namespace Hiscary.Stories.Domain.DataAccess;

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