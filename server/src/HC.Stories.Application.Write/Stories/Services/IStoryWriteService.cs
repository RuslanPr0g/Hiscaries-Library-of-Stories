using HC.Application.Write.Stories.Command;

namespace HC.Application.Write.Stories.Services;

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