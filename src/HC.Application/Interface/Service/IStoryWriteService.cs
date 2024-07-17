using HC.Application.Models.Response;
using HC.Application.Stories.Command;
using HC.Application.Stories.Command.ReadStory;
using HC.Application.Stories.Command.ScoreStory;
using HC.Application.Stories.DeleteStory;
using HC.Domain.Stories;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IStoryWriteService
{
    Task<Story> GetStoryById(StoryId storyId);
    Task<PublishStoryResult> PublishStory(CreateStoryCommand command);
    Task<int> DeleteStory(DeleteStoryCommand command);
    Task<int> DeleteComment(DeleteCommentCommand command);
    Task<PublishStoryResult> UpdateStory(UpdateStoryCommand command);
    Task<int> AddComment(AddCommentCommand command);
    Task AddStoryScore(StoryScoreCommand command);
    Task<PublishStoryResult> BookmarkStory(BookmarkStoryCommand command);
    Task AddImageToStory(StoryId storyId, string imagePath);
    Task AddImageToStoryByBase64(StoryId storyId, byte[] base64);
    Task<int> ReadStoryHistory(ReadStoryCommand command);
}