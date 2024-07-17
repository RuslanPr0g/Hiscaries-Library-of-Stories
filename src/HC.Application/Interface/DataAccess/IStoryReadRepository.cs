using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IStoryReadRepository
{
    // USE DAPPER HERE TO RETURN THE READ MODELS AS VIEWS, ETC.
    Task<IReadOnlyCollection<StoryReadModel>> GetStories();

    Task<StoryReadModel> GetStory(int storyId);

    Task<List<GenreReadModel>> GetGenres();

    Task<List<StoryBookMarkReadModel>> GetBookMarks();

    /// <summary>
    /// Get all audios associated with this story
    /// </summary>
    /// <param name="storyId">Story identifier</param>
    /// <returns>List of audios</returns>
    Task<List<StoryAudioReadModel>> GetAudio(int storyId);

    Task<StoryAudioReadModel> GetAudioById(int audioId);
}