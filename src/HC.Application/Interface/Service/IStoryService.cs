using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Application.DTOs;
using HC.Application.Models.Response;

using HC.Domain.Story;
using HC.Domain.Story.Comment;

namespace HC.Application.Interface;

public interface IStoryService
{
    Task<PublishStoryResult> PublishStory(Story story);
    Task<List<Genre>> GetBookMarks();
    Task<List<StoryReadHistoryProgress>> GetHistory(int userId);
    Task<int> DeleteStory(int story);
    Task<List<StoryRating>> GetStoryScores();
    Task<int> UpdateComment(Comment comment);
    Task<int> DeleteComment(Comment comment);
    Task<List<StoryRating>> GetScores();
    Task<PublishStoryResult> UpdateStory(Story story);
    Task<int> AddComment(Comment comment);
    Task<List<Story>> GetStoryBookMarksByUserId(int userId);
    Task<List<Comment>> GetAllComments();
    Task UpdateStoryScore(StoryRating storyScore);
    Task<Story> GetStoryById(int storyId);
    Task AddStoryScore(StoryRating storyScore);
    Task<List<StoryReadDto>> GetAllStories();
    Task<PublishStoryResult> BookmarkStory(StoryBookMark bookMark);
    Task AddImageToStory(int storyId, string imagePath);
    Task AddImageToStoryByBase64(int storyId, byte[] base64);
    Task<int> ReadStoryHistory(StoryReadHistory story);
}