using System.Collections.Generic;
using System.Threading.Tasks;

using HC.Domain.Story;
using HC.Domain.Story.Comment;

namespace HC.Application.Interface;

public interface IStoryRepository
{
    /// <summary>
    /// Get stories
    /// </summary>
    Task<IReadOnlyCollection<Story>> GetStories();

    /// <summary>
    /// Get story by id
    /// </summary>
    Task<Story> GetStory(int storyId);

    /// <summary>
    /// Get number of pages which story has
    /// </summary>
    Task<int> GetNumberOfPages(int storyId);

    /// <summary>
    /// Get all the pages by story id
    /// </summary>
    Task<List<StoryPage>> GetStoryPages(int storyId);

    Task<int> GetStoryPagesCount(int storyId);

    Task<int> DeleteStory(int storyId);

    /// <summary>
    /// Add story to database
    /// </summary>
    /// <returns>Id of added story</returns>
    Task<int> AddStory(Story story);

    Task<List<StoryRating>> GetStoryScores();
    Task<List<Comment>> GetStoryComments();
    Task<List<StoryReadHistory>> GetStoryReadHistory();
    Task<List<StoryReadHistoryProgress>> GetStoryReadHistoryProgress(int userId);

    /// <summary>
    /// Add page of story to database
    /// </summary>
    /// <returns>Id of added page</returns>
    Task<int> AddPage(StoryPage page);

    /// <summary>
    /// Updates a story
    /// </summary>
    /// <param name="story">Story to update</param>
    /// <returns>Task</returns>
    Task UpdateStory(Story story);

    Task RemovePagesFromStory(int storyId);
    Task<int> ReadStoryHistory(StoryReadHistory story);
    Task<int> TotalReadsForUser(int id);
    Task<int> TotalStoriesForUser(int id);
    Task<double> AverageScoreForUser(int id);
    Task<List<Genre>> GetGenres();
    Task<List<StoryBookMark>> GetBookMarks();
    Task UnBookMarkStory(StoryBookMark bookMark);
    Task BookMarkStory(StoryBookMark bookMark);
    Task<List<Story>> GetStoryBookMarks(int userId);
    Task<int> AddStoryComment(Comment comment);
    Task<int> AddGenre(Genre genre);
    Task<int> UpdateGenre(Genre genre);
    Task<int> DeleteGenre(Genre genre);
    Task<int> UpdateStoryComment(Comment comment);
    Task<int> DeleteStoryComment(Comment comment);
    Task UpdateStoryScore(StoryRating storyScore);
    Task InsertStoryScore(StoryRating storyScore);
    /// <summary>
    /// Get all audios associated with this story
    /// </summary>
    /// <param name="storyId">Story identifier</param>
    /// <param name="user">User connection</param>
    /// <returns>List of audios</returns>
    Task<List<StoryAudio>> GetAudio(int storyId);
    Task<bool> DeleteAudio(int[] audioIds);
    Task<int> CreateAudio(StoryAudio storyAudio);
    Task<StoryAudio> GetAudioById(int audioId);
}