using HC.Stories.Domain.ReadModels;

public interface IStoryReadService
{
    Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId, Guid searchedBy);

    Task<IEnumerable<GenreReadModel>> GetAllGenres();

    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(Guid userId);

    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(Guid userId);

    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(Guid userId);

    Task<IEnumerable<StorySimpleReadModel>> SearchForStory(
        Guid userId,
        Guid? storyId = null,
        Guid? libraryId = null,
        string? searchTerm = null,
        string? genre = null,
        string? requesterUsername = null);
}