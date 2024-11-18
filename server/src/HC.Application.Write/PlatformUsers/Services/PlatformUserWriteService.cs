using HC.Application.Constants;
using HC.Application.Write.Generators;
using HC.Application.Write.PlatformUsers.Command.DeleteReview;
using HC.Application.Write.PlatformUsers.Command.PublishReview;
using HC.Application.Write.PlatformUsers.DataAccess;
using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Command;
using HC.Domain.PlatformUsers;
using HC.Domain.UserAccounts;
using Microsoft.Extensions.Logging;

namespace HC.Application.Write.PlatformUsers.Services;

public sealed class PlatformUserWriteService : IPlatformUserWriteService
{
    private readonly IPlatformUserWriteRepository _repository;
    private readonly IIdGenerator _idGenerator;
    private readonly ILogger<PlatformUserWriteService> _logger;

    public PlatformUserWriteService(
        IPlatformUserWriteRepository repository,
        IIdGenerator idGenerator,
        ILogger<PlatformUserWriteService> logger)
    {
        _repository = repository;
        _idGenerator = idGenerator;
        _logger = logger;
    }

    public async Task<OperationResult> BookmarkStory(BookmarkStoryCommand command)
    {
        _logger.LogInformation("Attempting to bookmark story for user {UserId}", command.UserId);
        PlatformUser? user = await _repository.GetByUserAccountId(command.UserId);

        if (user is null)
        {
            _logger.LogWarning("User {UserId} not found when attempting to bookmark story", command.UserId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.BookmarkStory(
            _idGenerator.Generate((id) => new StoryBookMarkId(id)),
            command.StoryId);

        _logger.LogInformation("Successfully bookmarked story {StoryId} for user {UserId}", command.StoryId, command.UserId);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> ReadStoryHistory(ReadStoryCommand command)
    {
        _logger.LogInformation("Attempting to record read story history for user {UserId}, story {StoryId}, page {Page}", command.UserId, command.StoryId, command.Page);
        PlatformUser? user = await _repository.GetByUserAccountId(command.UserId);

        if (user is null)
        {
            _logger.LogWarning("Failed to record read story history: User {UserId} not found", command.UserId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.ReadStoryPage(
            command.StoryId,
            command.Page);

        _logger.LogInformation("Successfully recorded read story history for user {UserId}, story {StoryId}, page {Page}", command.UserId, command.StoryId, command.Page);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> BecomePublisher(UserAccountId userId)
    {
        _logger.LogInformation("Attempting to make user {id} a publisher", userId);
        PlatformUser? user = await _repository.GetByUserAccountId(userId);

        if (user is null)
        {
            _logger.LogWarning("Failed to make user a publisher: User {id} not found", userId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        var libraryId = _idGenerator.Generate((Guid id) => new LibraryId(id));
        user.BecomePublisher(libraryId);
        _logger.LogInformation("Successfully made user {id} a publisher", userId);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> PublishReview(PublishReviewCommand command)
    {
        _logger.LogInformation("Attempting to publish review for reviewer {ReviewerId}, publisher {LibraryId}", command.ReviewerId, command.LibraryId);
        PlatformUser? user = await _repository.GetByUserAccountId(command.ReviewerId);

        if (user is null)
        {
            _logger.LogWarning("Failed to publish review: User {ReviewerId} not found", command.ReviewerId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.PublishReview(
            command.LibraryId,
            command.Message);

        _logger.LogInformation("Successfully published review for reviewer {ReviewerId}, publisher {LibraryId}", command.ReviewerId, command.LibraryId);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> DeleteReview(DeleteReviewCommand command)
    {
        _logger.LogInformation("Attempting to delete review {ReviewId} for user {UserId}", command.LibraryId, command.UserId);
        PlatformUser? user = await _repository.GetByUserAccountId(command.UserId);

        if (user is null)
        {
            _logger.LogWarning("Failed to delete review: User {UserId} not found", command.UserId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.RemoveReview(command.LibraryId);
        _logger.LogInformation("Successfully deleted review {ReviewId} for user {UserId}", command.LibraryId, command.UserId);
        return OperationResult.CreateSuccess();
    }
}
