using HC.Stories.Application.Write.Stories.Command.AddComment;
using HC.Stories.Application.Write.Stories.Command.CreateGenre;
using HC.Stories.Application.Write.Stories.Command.DeleteComment;
using HC.Stories.Application.Write.Stories.Command.DeleteGenre;
using HC.Stories.Application.Write.Stories.Command.DeleteStory;
using HC.Stories.Application.Write.Stories.Command.DeleteStoryAudio;
using HC.Stories.Application.Write.Stories.Command.PublishStory;
using HC.Stories.Application.Write.Stories.Command.ScoreStory;
using HC.Stories.Application.Write.Stories.Command.UpdateComment;
using HC.Stories.Application.Write.Stories.Command.UpdateGenre;
using HC.Stories.Application.Write.Stories.Command.UpdateStory;
using HC.Stories.Application.Write.Stories.Command.UpdateStoryAudio;

namespace HC.Stories.Application.Write.Stories.Services;

public interface IStoryWriteService
{
    Task<OperationResult<EntityIdResponse>> PublishStory(PublishStoryCommand command);
    Task<OperationResult> UpdateStory(UpdateStoryCommand command);
    Task<OperationResult> DeleteStory(DeleteStoryCommand command);

    Task<OperationResult> DeleteAudio(DeleteStoryAudioCommand request);
    Task<OperationResult> UpdateAudio(UpdateStoryAudioCommand request);

    Task<OperationResult> AddComment(AddCommentCommand command);
    Task<OperationResult> UpdateComment(UpdateCommentCommand request);
    Task<OperationResult> DeleteComment(DeleteCommentCommand command);

    Task<OperationResult> SetStoryScoreForAUser(StoryScoreCommand command);

    Task<OperationResult> CreateGenre(CreateGenreCommand request);
    Task<OperationResult> UpdateGenre(UpdateGenreCommand request);
    Task<OperationResult> DeleteGenre(DeleteGenreCommand request);
}