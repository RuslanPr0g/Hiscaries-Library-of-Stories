using Hiscary.Stories.Domain.ReadModels;
using Hiscary.Stories.Domain.Stories;

public interface IStoryReadService
{
    HashSet<string> SortableProperties { get; }

    Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId);

    Task<IEnumerable<StorySimpleReadModel>> GetStoryByIds(
        StoryId[] storyIds,
        string sortProperty = "CreatedAt",
        bool sortAsc = true);

    Task<IEnumerable<GenreReadModel>> GetAllGenres();

    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations();

    Task<IEnumerable<StorySimpleReadModel>> SearchForStory(
        Guid? storyId = null,
        Guid? libraryId = null,
        string? searchTerm = null,
        string? genre = null);
}