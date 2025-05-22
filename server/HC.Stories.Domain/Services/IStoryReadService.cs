using HC.Stories.Domain.ReadModels;

public interface IStoryReadService
{
    Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId);

    Task<IEnumerable<GenreReadModel>> GetAllGenres();

    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations();

    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading();

    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory();

    Task<IEnumerable<StorySimpleReadModel>> SearchForStory(
        Guid? storyId = null,
        Guid? libraryId = null,
        string? searchTerm = null,
        string? genre = null);
}