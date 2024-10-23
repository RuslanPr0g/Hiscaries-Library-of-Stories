using HC.Application.Common.Constants;
using HC.Application.Interface;
using HC.Application.Interface.Generators;
using HC.Application.Models.Response;
using HC.Application.ResultModels.Response;
using HC.Application.Stories.Command;
using HC.Application.Stories.Command.DeleteStory;
using HC.Application.Stories.Command.ScoreStory;
using HC.Domain.Stories;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public sealed class StoryWriteService : IStoryWriteService
{
    private readonly IStoryWriteRepository _repository;
    private readonly IIdGenerator _idGenerator;
    private readonly ILogger<StoryWriteService> _logger;

    public StoryWriteService(
        IStoryWriteRepository storyWriteRepository,
        IIdGenerator idGenerator,
        ILogger<StoryWriteService> logger)
    {
        _repository = storyWriteRepository;
        _idGenerator = idGenerator;
        _logger = logger;
    }

    public async Task<OperationResult> AddComment(AddCommentCommand command)
    {
        _logger.LogInformation("Adding comment to story {StoryId} by user {UserId}", command.StoryId, command.UserId);
        var story = await _repository.GetStory(command.StoryId);

        if (story is null)
        {
            _logger.LogWarning("Story {StoryId} not found when adding comment", command.StoryId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.StoryWasNotFound);
        }

        var commentId = _idGenerator.Generate((id) => new CommentId(id));
        story.AddComment(commentId, command.UserId, command.Content, command.Score, DateTime.UtcNow);
        _logger.LogInformation("Comment {CommentId} added to story {StoryId}", commentId, command.StoryId);

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> SetStoryScoreForAUser(StoryScoreCommand command)
    {
        _logger.LogInformation("Setting score {Score} for story {StoryId} by user {UserId}", command.Score, command.StoryId, command.UserId);
        var story = await _repository.GetStory(command.StoryId);

        if (story is null)
        {
            _logger.LogWarning("Story {StoryId} not found when setting score", command.StoryId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.StoryWasNotFound);
        }

        var ratingId = _idGenerator.Generate((id) => new StoryRatingId(id));
        story.SetScoreByUser(command.UserId, command.Score, ratingId);
        _logger.LogInformation("Score set for story {StoryId} with rating ID {RatingId}", command.StoryId, ratingId);

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> CreateGenre(CreateGenreCommand command)
    {
        _logger.LogInformation("Creating new genre: {GenreName}", command.Name);
        var genreId = _idGenerator.Generate((id) => new GenreId(id));
        var genre = Genre.Create(genreId, command.Name, command.Description, command.ImagePreview);

        await _repository.AddGenre(genre);
        _logger.LogInformation("Genre created with ID {GenreId}", genreId);

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> UpdateGenre(UpdateGenreCommand command)
    {
        _logger.LogInformation("Updating genre {GenreId}", command.GenreId);
        var genre = await _repository.GetGenre(command.GenreId);

        if (genre is null)
        {
            _logger.LogWarning("Genre {GenreId} not found when updating", command.GenreId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.GenreWasNotFound);
        }

        genre.UpdateInformation(command.Name, command.Description, command.ImagePreview);
        _logger.LogInformation("Genre {GenreId} updated successfully", command.GenreId);

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> UpdateComment(UpdateCommentCommand command)
    {
        _logger.LogInformation("Updating comment {CommentId} for story {StoryId}", command.CommentId, command.StoryId);
        var story = await _repository.GetStory(command.StoryId);

        if (story is null)
        {
            _logger.LogWarning("Story {StoryId} not found when updating comment", command.StoryId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.StoryWasNotFound);
        }

        story.UpdateComment(command.CommentId, command.Content, command.Score, DateTime.UtcNow);
        _logger.LogInformation("Comment {CommentId} updated for story {StoryId}", command.CommentId, command.StoryId);

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult<EntityIdResponse>> PublishStory(PublishStoryCommand command)
    {
        _logger.LogInformation("Publishing new story: {StoryTitle} by {AuthorName}", command.Title, command.AuthorName);
        var storyId = _idGenerator.Generate((id) => new StoryId(id));

        var existingGenres = await _repository.GetGenresByIds(command.GenreIds.ToArray());
        
        var story = Story.Create(
            storyId,
            command.PublisherId,
            command.Title,
            command.Description,
            command.AuthorName,
            existingGenres,
            command.AgeLimit,
            command.ImagePreview,
            DateTime.UtcNow,
            command.DateWritten);

        await _repository.AddStory(story);
        _logger.LogInformation("Story published with ID {StoryId}", storyId);

        return OperationResult<EntityIdResponse>.CreateSuccess(new EntityIdResponse { Id = story.Id });
    }

    public async Task<OperationResult> UpdateStory(UpdateStoryCommand command)
    {
        _logger.LogInformation("Updating story {StoryId}", command.StoryId);
        var story = await _repository.GetStory(command.StoryId);

        if (story is null)
        {
            _logger.LogWarning("Story {StoryId} not found when updating", command.StoryId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.StoryWasNotFound);
        }

        if (story.PublisherId.Value != command.PublisherId)
        {
            _logger.LogWarning("Story {StoryId} can only be updated by its publisher or an administrator, user {UserId} tried to update not his story.", 
                command.StoryId, command.PublisherId);
            return OperationResult.CreateUnauthorizedError(UserFriendlyMessages.NoRights);
        }

        story.UpdateInformation(
            command.Title,
            command.Description,
            command.AuthorName,
            command.GenreIds.Select(id => Genre.Create(id)).ToList(),
            command.AgeLimit,
            command.ImagePreview,
            DateTime.UtcNow,
            command.DateWritten);

        if (command.Contents is not null && command.Contents.Any())
        {
            // TODO: add wrapper around datetime.
            var editedAt = DateTime.Now;
            story.ModifyContents(command.Contents, editedAt);
        }

        _logger.LogInformation("Story {StoryId} updated successfully", command.StoryId);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> UpdateAudio(UpdateStoryAudioCommand command)
    {
        _logger.LogInformation("Updating audio for story {StoryId}", command.StoryId);
        var story = await _repository.GetStory(command.StoryId);

        if (story is null)
        {
            _logger.LogWarning("Story {StoryId} not found when updating audio", command.StoryId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.StoryWasNotFound);
        }

        Guid fileId = Guid.NewGuid();
        var audioId = _idGenerator.Generate((id) => new StoryAudioId(id));
        story.SetAudio(audioId, fileId, DateTime.UtcNow, command.Name);

        try
        {
            SaveByteArrayToFileWithBinaryWriter(command.Audio, $"audios/{fileId}.mp3");
            _logger.LogInformation("Audio file saved for story {StoryId} with file ID {FileId}", command.StoryId, fileId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving audio file for story {StoryId}", command.StoryId);
            return OperationResult.CreateClientSideError("Error saving audio file");
        }

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> DeleteAudio(DeleteStoryAudioCommand command)
    {
        _logger.LogInformation("Deleting audio for story {StoryId}", command.StoryId);
        var story = await _repository.GetStory(command.StoryId);

        if (story is null)
        {
            _logger.LogWarning("Story {StoryId} not found when deleting audio", command.StoryId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.StoryWasNotFound);
        }

        Guid? removedAudioId = story.ClearAllAudio();

        if (removedAudioId.HasValue)
        {
            string path = $"audios/{removedAudioId}.mp3";

            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    _logger.LogInformation("Audio file deleted for story {StoryId}", command.StoryId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting audio file for story {StoryId}", command.StoryId);
                }
            }
            else
            {
                _logger.LogWarning("Audio file not found for story {StoryId}", command.StoryId);
            }
        }

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> DeleteComment(DeleteCommentCommand command)
    {
        _logger.LogInformation("Deleting comment {CommentId} from story {StoryId}", command.CommentId, command.StoryId);
        var story = await _repository.GetStory(command.StoryId);

        if (story is null)
        {
            _logger.LogWarning("Story {StoryId} not found when deleting comment", command.StoryId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.StoryWasNotFound);
        }

        story.DeleteComment(command.CommentId);
        _logger.LogInformation("Comment {CommentId} deleted from story {StoryId}", command.CommentId, command.StoryId);

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> DeleteGenre(DeleteGenreCommand command)
    {
        _logger.LogInformation("Deleting genre {GenreId}", command.GenreId);
        var genre = await _repository.GetGenre(command.GenreId);

        if (genre is null)
        {
            _logger.LogWarning("Genre {GenreId} not found when deleting", command.GenreId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.GenreWasNotFound);
        }

        _repository.DeleteGenre(genre);
        _logger.LogInformation("Genre {GenreId} deleted successfully", command.GenreId);

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> DeleteStory(DeleteStoryCommand command)
    {
        _logger.LogInformation("Deleting story {StoryId}", command.StoryId);
        var story = await _repository.GetStory(command.StoryId);

        if (story is not null)
        {
            _repository.DeleteStory(story);
            _logger.LogInformation("Story {StoryId} deleted successfully", command.StoryId);
        }
        else
        {
            _logger.LogWarning("Story {StoryId} not found when deleting", command.StoryId);
        }

        return OperationResult.CreateSuccess();
    }

    private static void SaveByteArrayToFileWithBinaryWriter(byte[] data, string filePath)
    {
        using BinaryWriter writer = new(File.OpenWrite(filePath));
        writer.Write(data);
    }
}
