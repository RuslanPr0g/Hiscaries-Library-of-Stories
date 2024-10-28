using HC.Application.Constants;
using HC.Application.Options;
using HC.Application.Tokens;
using HC.Application.Write.Generators;
using HC.Application.Write.JWT;
using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Command;
using HC.Application.Write.Users.Command;
using HC.Application.Write.Users.DataAccess;
using HC.Domain.Users;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HC.Application.Write.Users.Services;

public sealed class UserWriteService : IUserWriteService
{
    private readonly IUserWriteRepository _repository;
    private readonly IIdGenerator _idGenerator;
    private readonly JwtSettings _jwtSettings;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IJWTTokenHandler _tokenHandler;
    private readonly ILogger<UserWriteService> _logger;
    private readonly SaltSettings _saltSettings;

    public UserWriteService(
        IUserWriteRepository repository,
        JwtSettings jwtSettings,
        TokenValidationParameters tokenValidationParameters,
        IIdGenerator idGenerator,
        IJWTTokenHandler tokenHandler,
        ILogger<UserWriteService> logger,
        SaltSettings saltSettings)
    {
        _repository = repository;
        _jwtSettings = jwtSettings;
        _tokenValidationParameters = tokenValidationParameters;
        _idGenerator = idGenerator;
        _tokenHandler = tokenHandler;
        _logger = logger;
        _saltSettings = saltSettings;
    }

    public async Task<OperationResult> BookmarkStory(BookmarkStoryCommand command)
    {
        _logger.LogInformation("Attempting to bookmark story for user {UserId}", command.UserId);
        User? user = await _repository.GetUserById(command.UserId);

        if (user is null)
        {
            _logger.LogWarning("User {UserId} not found when attempting to bookmark story", command.UserId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.BookmarkStory(
            _idGenerator.Generate((id) => new UserStoryBookMarkId(id)),
            command.StoryId,
            DateTime.UtcNow);

        _logger.LogInformation("Successfully bookmarked story {StoryId} for user {UserId}", command.StoryId, command.UserId);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> ReadStoryHistory(ReadStoryCommand command)
    {
        _logger.LogInformation("Attempting to record read story history for user {UserId}, story {StoryId}, page {Page}", command.UserId, command.StoryId, command.Page);
        User? user = await _repository.GetUserById(command.UserId);

        if (user is null)
        {
            _logger.LogWarning("Failed to record read story history: User {UserId} not found", command.UserId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.ReadStoryPage(
            command.StoryId,
            command.Page,
            DateTime.UtcNow,
            _idGenerator.Generate((id) => new UserReadHistoryId(id)));

        _logger.LogInformation("Successfully recorded read story history for user {UserId}, story {StoryId}, page {Page}", command.UserId, command.StoryId, command.Page);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> BecomePublisher(string username)
    {
        _logger.LogInformation("Attempting to make user {Username} a publisher", username);
        User? user = await _repository.GetUserByUsername(username);

        if (user is null)
        {
            _logger.LogWarning("Failed to make user a publisher: User {Username} not found", username);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.BecomePublisher();
        _logger.LogInformation("Successfully made user {Username} a publisher", username);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> PublishReview(PublishReviewCommand command)
    {
        _logger.LogInformation("Attempting to publish review for reviewer {ReviewerId}, publisher {PublisherId}", command.ReviewerId, command.PublisherId);
        User? user = await _repository.GetUserById(command.ReviewerId);

        if (user is null)
        {
            _logger.LogWarning("Failed to publish review: User {ReviewerId} not found", command.ReviewerId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        if (command.ReviewId.HasValue)
        {
            _logger.LogInformation("Republishing existing review {ReviewId}", command.ReviewId.Value);
            user.RePublishReview(
                command.PublisherId,
                command.Message,
                command.ReviewId.Value);
        }
        else
        {
            _logger.LogInformation("Publishing new review");
            user.PublishNewReview(
                new UserId(command.PublisherId),
                command.Message,
                _idGenerator.Generate((id) => new ReviewId(id)));
        }

        _logger.LogInformation("Successfully published review for reviewer {ReviewerId}, publisher {PublisherId}", command.ReviewerId, command.PublisherId);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult> DeleteReview(DeleteReviewCommand command)
    {
        _logger.LogInformation("Attempting to delete review {ReviewId} for user {Username}", command.ReviewId, command.Username);
        User? user = await _repository.GetUserByUsername(command.Username);

        if (user is null)
        {
            _logger.LogWarning("Failed to delete review: User {Username} not found", command.Username);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.RemoveReview(new ReviewId(command.ReviewId));
        _logger.LogInformation("Successfully deleted review {ReviewId} for user {Username}", command.ReviewId, command.Username);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult<User>> GetUserById(UserId userId)
    {
        _logger.LogInformation("Attempting to get user by ID {UserId}", userId);
        User? user = await _repository.GetUserById(userId);

        if (user is null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return OperationResult<User>.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        _logger.LogInformation("Successfully retrieved user {UserId}", userId);
        return OperationResult<User>.CreateSuccess(user);
    }

    public async Task<OperationResult<User>> GetUserByUsername(string username)
    {
        _logger.LogInformation("Attempting to get user by username {Username}", username);
        User? user = await _repository.GetUserByUsername(username);

        if (user is null)
        {
            _logger.LogWarning("User not found: {Username}", username);
            return OperationResult<User>.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        _logger.LogInformation("Successfully retrieved user {Username}", username);
        return OperationResult<User>.CreateSuccess(user);
    }

    public async Task<OperationResult<UserWithTokenResponse>> LoginUser(LoginUserCommand command)
    {
        _logger.LogInformation("Attempting to log in user {Username}", command.Username);
        User? user = await _repository.GetUserByUsername(command.Username);

        if (user is null)
        {
            _logger.LogWarning("Login attempt failed: User {Username} not found", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateNotFound(UserFriendlyMessages.UserIsNotFound);
        }

        var passwordMatch = HashPassword(command.Password, _saltSettings.StoredSalt) == user.Password;

        if (passwordMatch is false)
        {
            _logger.LogWarning("Login attempt failed: Password mismatch for user {Username}", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateValidationsError(UserFriendlyMessages.PasswordMismatch);
        }

        if (user.Banned)
        {
            _logger.LogWarning("Login attempt failed: User {Username} is banned", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateValidationsError(UserFriendlyMessages.UserIsBanned);
        }

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(user, _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate JWT token for user {Username}", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        user.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        _logger.LogInformation("User {Username} successfully logged in", command.Username);
        return OperationResult<UserWithTokenResponse>.CreateSuccess(new UserWithTokenResponse
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult<UserWithTokenResponse>> RegisterUser(RegisterUserCommand command)
    {
        _logger.LogInformation("Attempting to register new user with username {Username}", command.Username);

        User? existingUser = await _repository.GetUserByUsername(command.Username);

        if (existingUser is not null)
        {
            _logger.LogWarning("Registration failed: Username {Username} already exists", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateValidationsError(UserFriendlyMessages.UserWithUsernameExists);
        }

        var exists = await _repository.IsUserExistByEmail(command.Email);

        if (exists)
        {
            _logger.LogWarning("Registration failed: Email {Email} already in use", command.Email);
            return OperationResult<UserWithTokenResponse>.CreateValidationsError(UserFriendlyMessages.UserWithEmailExists);
        }

        string encryptedpassword = HashPassword(command.Password, _saltSettings.StoredSalt);

        var userId = _idGenerator.Generate((id) => new UserId(id));
        var createdUser = new User(
            userId,
            command.Username,
            command.Email,
            encryptedpassword,
            DateTime.UtcNow,
            DateTime.UtcNow
            );

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(createdUser, _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate JWT token for new user {Username}", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        createdUser.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        await _repository.AddUser(createdUser);

        _logger.LogInformation("Successfully registered new user {Username} with ID {UserId}", command.Username, userId);
        return OperationResult<UserWithTokenResponse>.CreateSuccess(new UserWithTokenResponse
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult<UserWithTokenResponse>> RefreshToken(RefreshTokenCommand command)
    {
        _logger.LogInformation("Attempting to refresh token for user {Username}", command.Username);
        User? user = await _repository.GetUserByUsername(command.Username);

        if (user is null)
        {
            _logger.LogWarning("Token refresh failed: User {Username} not found", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateValidationsError(UserFriendlyMessages.UserIsNotFound);
        }

        var validatedToken = _tokenHandler.GetTokenWithClaims(command.Token, _tokenValidationParameters);

        if (validatedToken is null)
        {
            _logger.LogWarning("Token refresh failed: Invalid token for user {Username}", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateValidationsError(UserFriendlyMessages.PleaseRelogin);
        }

        if (validatedToken.ExpiryDate > DateTime.UtcNow)
        {
            _logger.LogWarning("Token refresh failed: Token not expired for user {Username}", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateValidationsError(UserFriendlyMessages.RefreshTokenIsNotExpired);
        }

        bool validated = user.ValidateRefreshToken(validatedToken.JTI);

        if (!validated)
        {
            _logger.LogWarning("Token refresh failed: Invalid refresh token for user {Username}", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateClientSideError(UserFriendlyMessages.RefreshTokenIsExpired);
        }

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(user, _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate new JWT token for user {Username}", command.Username);
            return OperationResult<UserWithTokenResponse>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        user.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        _logger.LogInformation("Successfully refreshed token for user {Username}", command.Username);
        return OperationResult<UserWithTokenResponse>.CreateSuccess(new UserWithTokenResponse
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult> UpdateUserData(UpdateUserDataCommand command)
    {
        _logger.LogInformation("Attempting to update user data for {Username}", command.Username);
        User? user = await _repository.GetUserByUsername(command.Username);

        if (user is null)
        {
            _logger.LogWarning("Update user data failed: User {Username} not found", command.Username);
            return OperationResult.CreateValidationsError(UserFriendlyMessages.UserIsNotFound);
        }

        if (!string.IsNullOrEmpty(command.UpdatedUsername))
        {
            _logger.LogInformation("Checking if new username {UpdatedUsername} is available", command.UpdatedUsername);
            var newUsernameUser = await _repository.GetUserByUsername(command.UpdatedUsername);

            if (newUsernameUser is not null)
            {
                _logger.LogWarning("Update user data failed: New username {UpdatedUsername} already exists", command.UpdatedUsername);
                return OperationResult.CreateValidationsError(UserFriendlyMessages.UserWithUsernameExists);
            }
        }

        var existsWithEmail = await _repository.IsUserExistByEmail(command.Email);

        if (existsWithEmail)
        {
            _logger.LogWarning("Update user data failed: Email {Email} already in use", command.Email);
            return OperationResult.CreateValidationsError(UserFriendlyMessages.UserWithEmailExists);
        }

        if (!string.IsNullOrEmpty(command.NewPassword))
        {
            _logger.LogInformation("Attempting to update password for user {Username}", command.Username);
            var result = UpdateUserPassword(user, command.PreviousPassword, command.NewPassword);

            if (result.ResultStatus is ResultStatus.Success)
            {
                _logger.LogInformation("Password updated successfully for user {Username}", command.Username);
                user.UpdatePersonalInformation(command.UpdatedUsername, command.Email, command.BirthDate);
            }
            else
            {
                _logger.LogWarning("Password update failed for user {Username}", command.Username);
            }

            return result;
        }
        else
        {
            user.UpdatePersonalInformation(command.UpdatedUsername, command.Email, command.BirthDate);
        }

        _logger.LogInformation("Successfully updated user data for {Username}", command.Username);
        return OperationResult.CreateSuccess();
    }

    private OperationResult UpdateUserPassword(User user, string previousPassword, string newPassword)
    {
        _logger.LogDebug("Attempting to update password for user {UserId}", user.Id);
        var hashedPreviousPassword = HashPassword(previousPassword, _saltSettings.StoredSalt);

        if (user.Password != hashedPreviousPassword)
        {
            _logger.LogWarning("Password update failed: Previous password mismatch for user {UserId}", user.Id);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.PasswordMismatch);
        }

        var hashedNewPassword = HashPassword(newPassword, _saltSettings.StoredSalt);

        user.UpdatePassword(hashedNewPassword);
        _logger.LogInformation("Password updated successfully for user {UserId}", user.Id);
        return OperationResult.CreateSuccess();
    }

    private string HashPassword(string password, string salt)
    {
        _logger.LogDebug("Hashing password with salt");
        return BCrypt.Net.BCrypt.HashPassword(password, _saltSettings.StoredSalt);
    }
}
