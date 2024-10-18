using HC.Application.Models.Response;
using HC.Application.ResultModels.Response;
using HC.Application.Stories.Command;
using HC.Application.Stories.Command.DeleteStory;
using HC.Application.Stories.Command.ScoreStory;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IStoryWriteService
{
    Task<OperationResult<EntityIdResponse>> PublishStory(CreateStoryCommand command);
    Task<OperationResult> DeleteStory(DeleteStoryCommand command);
    Task<OperationResult> UpdateComment(UpdateCommentCommand request);
    Task<OperationResult> DeleteComment(DeleteCommentCommand command);
    Task<OperationResult> UpdateStory(UpdateStoryCommand command);
    Task<OperationResult> AddComment(AddCommentCommand command);
    Task<OperationResult> SetStoryScoreForAUser(StoryScoreCommand command);
    Task<OperationResult> CreateGenre(CreateGenreCommand request);
    Task<OperationResult> UpdateGenre(UpdateGenreCommand request);
    Task<OperationResult> DeleteGenre(DeleteGenreCommand request);
    Task<OperationResult> DeleteAudio(DeleteStoryAudioCommand request);
    Task<OperationResult> UpdateAudio(UpdateStoryAudioCommand request);
}