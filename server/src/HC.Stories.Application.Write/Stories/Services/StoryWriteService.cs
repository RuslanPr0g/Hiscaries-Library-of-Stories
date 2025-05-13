using HC.Application.Write.Stories.Command;
using HC.Application.Write.Stories.DataAccess;

namespace HC.Application.Write.Stories.Services;

public sealed class StoryWriteService : IStoryWriteService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IStoryWriteRepository _repository;
    private readonly IPlatformUserWriteRepository _userRepository;
    private readonly IImageUploader _imageUploader;
    private readonly IResourceUrlGeneratorService _urlGeneratorService;
    private readonly IIdGenerator _idGenerator;
    private readonly ILogger<StoryWriteService> _logger;

    public StoryWriteService(
        IStoryWriteRepository storyWriteRepository,
        IResourceUrlGeneratorService urlGeneratorService,
        IIdGenerator idGenerator,
        ILogger<StoryWriteService> logger,
        IFileStorageService fileStorageService,
        IPlatformUserWriteRepository userRepository,
        IImageUploader imageUploader)
    {
        _repository = storyWriteRepository;
        _urlGeneratorService = urlGeneratorService;
        _idGenerator = idGenerator;
        _logger = logger;
        _fileStorageService = fileStorageService;
        _userRepository = userRepository;
        _imageUploader = imageUploader;
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
        story.AddComment(commentId, command.UserId, command.Content, command.Score);
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

        story.SetScoreByUser(command.UserId, command.Score);
        _logger.LogInformation("Score set for story {StoryId} with rating ID {RatingId}", command.StoryId);

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

        // TODO: add a check that only the owner is going to update his comment

        story.UpdateComment(command.CommentId, command.Content, command.Score);
        _logger.LogInformation("Comment {CommentId} updated for story {StoryId}", command.CommentId, command.StoryId);

        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult<EntityIdResponse>> PublishStory(PublishStoryCommand command)
    {
        var platformUser = await _userRepository.GetByUserAccountId(command.UserId);

        if (platformUser is null)
        {
            _logger.LogWarning("Publishing new story is not possible: {StoryTitle} by {AuthorName}. User is not found {id}",
                command.Title,
                command.AuthorName,
                command.UserId);
            return OperationResult<EntityIdResponse>.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        var library = platformUser.GetCurrentLibrary();

        if (!platformUser.IsPublisher || library is null)
        {
            _logger.LogWarning("Publishing new story is not possible: {StoryTitle} by {AuthorName}. User is not a publisher {id}",
                command.Title,
                command.AuthorName,
                command.UserId);
            return OperationResult<EntityIdResponse>.CreateClientSideError(UserFriendlyMessages.NoRights);
        }

        _logger.LogInformation("Publishing new story: {StoryTitle} by {AuthorName}", command.Title, command.AuthorName);
        var storyId = _idGenerator.Generate((id) => new StoryId(id));

        var existingGenres = await _repository.GetGenresByIds(command.GenreIds.ToArray());

        string? imagePreviewUrl = await UploadStoryPreviewAndGetUrlAsync(storyId, command.ImagePreview);

        var story = Story.Create(
            storyId,
            library.Id,
            command.Title,
            command.Description,
            command.AuthorName,
            imagePreviewUrl,
            existingGenres,
            command.AgeLimit,
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

        if (story.Library.PlatformUserId.Value != command.CurrentUserId)
        {
            _logger.LogWarning("Story {StoryId} can only be updated by its publisher or an administrator, user {UserId} tried to update not his story.",
                command.StoryId, command.CurrentUserId);
            return OperationResult.CreateUnauthorizedError(UserFriendlyMessages.NoRights);
        }

        var existingGenres = await _repository.GetGenresByIds(command.GenreIds.ToArray());

        string? imagePreviewUrl = command.ShouldUpdateImage ?
            await UploadStoryPreviewAndGetUrlAsync(command.StoryId, command.ImagePreview) :
            story.ImagePreviewUrl;

        story.UpdateInformation(
            command.Title,
            command.Description,
            command.AuthorName,
            imagePreviewUrl,
            existingGenres,
            command.AgeLimit,
            DateTime.SpecifyKind(command.DateWritten, DateTimeKind.Utc));

        if (command.Contents is not null && command.Contents.Any())
        {
            story.ModifyContents(command.Contents);
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
        story.SetAudio(audioId, fileId, command.Name);

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

        // TODO: add rights check

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

        // TODO: add rights check

        if (story is not null)
        {
            // TODO: let's mark it as isDeleted and then have a
            // job that would remove all the isdeleted records once per month or smth
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

    public async Task<string?> UploadStoryPreviewAndGetUrlAsync(StoryId storyId, byte[] imagePreview)
    {
        if (imagePreview.Length == 0)
        {
            return null;
        }

        string fileName = await _imageUploader.UploadImageAsync(storyId, imagePreview, "stories");
        return _urlGeneratorService.GenerateImageUrlByFileName(fileName);
    }
}
