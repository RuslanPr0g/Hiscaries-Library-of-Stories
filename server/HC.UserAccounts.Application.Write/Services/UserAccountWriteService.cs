using Enterprise.Domain.Constants;
using Enterprise.Domain.Generators;
using Enterprise.Domain.Jwt;
using Enterprise.Domain.Options;
using Enterprise.Domain.ResultModels.Response;
using HC.UserAccounts.Application.Write.Extensions;
using HC.UserAccounts.Domain;
using HC.UserAccounts.Domain.DataAccess;
using HC.UserAccounts.Domain.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HC.UserAccounts.Application.Write.Services;

public sealed class UserAccountWriteService(
    IUserAccountWriteRepository repository,
    JwtSettings jwtSettings,
    TokenValidationParameters tokenValidationParameters,
    IIdGenerator idGenerator,
    IJWTTokenHandler tokenHandler,
    ILogger<UserAccountWriteService> logger,
    SaltSettings saltSettings) : IUserAccountWriteService
{
    private readonly IUserAccountWriteRepository _repository = repository;
    private readonly IIdGenerator _idGenerator = idGenerator;
    private readonly JwtSettings _jwtSettings = jwtSettings;
    private readonly TokenValidationParameters _tokenValidationParameters = tokenValidationParameters;
    private readonly IJWTTokenHandler _tokenHandler = tokenHandler;
    private readonly ILogger<UserAccountWriteService> _logger = logger;
    private readonly SaltSettings _saltSettings = saltSettings;

    public async Task<OperationResult> BanUser(UserAccountId userId)
    {
        UserAccount? user = await _repository.GetById(userId);

        if (user is null)
        {
            _logger.LogWarning("User {id} was not found to be banned", userId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.Ban();

        await _repository.SaveChanges();

        _logger.LogInformation("User {id} is now banned from using the system", userId);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult<TokenMetadata>> LoginUser(
        string username,
        string password)
    {
        _logger.LogInformation("Attempting to log in user {username}", username);
        UserAccount? user = await _repository.GetByUsername(username);

        if (user is null)
        {
            _logger.LogWarning("Login attempt failed: User {username} not found", username);
            return OperationResult<TokenMetadata>.CreateNotFound(UserFriendlyMessages.UserIsNotFound);
        }

        var passwordMatch = HashPassword(password, _saltSettings.StoredSalt) == user.Password;

        if (passwordMatch is false)
        {
            _logger.LogWarning("Login attempt failed: Password mismatch for user {username}", username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.PasswordMismatch);
        }

        if (user.IsBanned)
        {
            _logger.LogWarning("Login attempt failed: User {username} is banned", username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.UserIsBanned);
        }

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(
                user.Id,
                user.Email,
                user.Username,
                user.Role,
                _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate JWT token for user {username}", username);
            return OperationResult<TokenMetadata>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        user.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        await _repository.SaveChanges();

        _logger.LogInformation("User {username} successfully logged in", username);
        return OperationResult<TokenMetadata>.CreateSuccess(new TokenMetadata
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult<TokenMetadata>> RegisterUser(
        string username,
        string email,
        string password,
        DateTime birthDate)
    {
        _logger.LogInformation("Attempting to register new user with username {username}", username);

        UserAccount? existingUser = await _repository.GetByUsername(username);

        if (existingUser is not null)
        {
            _logger.LogWarning("Registration failed: username {username} already exists", username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.UserWithUsernameExists);
        }

        var exists = await _repository.IsExistByEmail(email);

        if (exists)
        {
            _logger.LogWarning("Registration failed: Email {Email} already in use", email);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.UserWithEmailExists);
        }

        string encryptedpassword = HashPassword(password, _saltSettings.StoredSalt);

        var userId = _idGenerator.Generate((id) => new UserAccountId(id));
        var createdUser = new UserAccount(
            userId,
            username,
            email,
            encryptedpassword,
            birthDate
            );

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(
                createdUser.Id,
                createdUser.Email,
                createdUser.Username,
                createdUser.Role,
                _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate JWT token for new user {username}", username);
            return OperationResult<TokenMetadata>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        createdUser.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        await _repository.Add(createdUser);

        await _repository.SaveChanges();

        _logger.LogInformation("Successfully registered new user {username} with ID {UserId}", username, userId);
        return OperationResult<TokenMetadata>.CreateSuccess(new TokenMetadata
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult<TokenMetadata>> RefreshToken(
        string username,
        string token,
        string refreshToken)
    {
        _logger.LogInformation("Attempting to refresh token for user {username}", username);
        UserAccount? user = await _repository.GetByUsername(username);

        if (user is null)
        {
            _logger.LogWarning("Token refresh failed: User {username} not found", username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.UserIsNotFound);
        }

        var validatedToken = _tokenHandler.GetTokenWithClaims(token, _tokenValidationParameters);

        if (validatedToken is null)
        {
            _logger.LogWarning("Token refresh failed: Invalid token for user {username}", username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.PleaseRelogin);
        }

        if (validatedToken.ExpiryDate > DateTime.UtcNow)
        {
            _logger.LogWarning("Token refresh failed: Token not expired for user {username}", username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.RefreshTokenIsNotExpired);
        }

        bool validated = user.ValidateRefreshToken(validatedToken.JTI);

        if (!validated)
        {
            _logger.LogWarning("Token refresh failed: Invalid refresh token for user {username}", username);
            return OperationResult<TokenMetadata>.CreateClientSideError(UserFriendlyMessages.RefreshTokenIsExpired);
        }

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(
                user.Id,
                user.Email,
                user.Username,
                user.Role,
                _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate new JWT token for user {username}", username);
            return OperationResult<TokenMetadata>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        user.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        await _repository.SaveChanges();

        _logger.LogInformation("Successfully refreshed token for user {username}", username);
        return OperationResult<TokenMetadata>.CreateSuccess(new TokenMetadata
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult> UpdateUserData(
        Guid id,
        string username,
        string email,
        DateTime birthDate,
        string previousPassword,
        string newPassword)
    {
        _logger.LogInformation("Attempting to update user data for {username}", username);
        UserAccount? user = await _repository.GetById(id);

        if (user is null)
        {
            _logger.LogWarning("Update user data failed: User {username} not found", username);
            return OperationResult.CreateValidationsError(UserFriendlyMessages.UserIsNotFound);
        }

        if (!string.IsNullOrEmpty(username))
        {
            _logger.LogInformation("Checking if new username {username} is available", username);
            var newusernameUser = await _repository.GetByUsername(username);

            if (newusernameUser is not null)
            {
                _logger.LogWarning("Update user data failed: New username {username} already exists", username);
                return OperationResult.CreateValidationsError(UserFriendlyMessages.UserWithUsernameExists);
            }
        }

        var existsWithEmail = await _repository.IsExistByEmail(email);

        if (existsWithEmail)
        {
            _logger.LogWarning("Update user data failed: Email {Email} already in use", email);
            return OperationResult.CreateValidationsError(UserFriendlyMessages.UserWithEmailExists);
        }

        if (!string.IsNullOrEmpty(newPassword))
        {
            _logger.LogInformation("Attempting to update password for user {username}", username);
            var result = UpdateUserPassword(user, previousPassword, newPassword);

            if (result.ResultStatus is ResultStatus.Success)
            {
                _logger.LogInformation("Password updated successfully for user {username}", username);
                user.UpdatePersonalInformation(username, email, birthDate);

                await _repository.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Password update failed for user {username}", username);
            }

            return result;
        }
        else
        {
            user.UpdatePersonalInformation(username, email, birthDate);
        }

        await _repository.SaveChanges();

        _logger.LogInformation("Successfully updated user data for {username}", username);
        return OperationResult.CreateSuccess();
    }

    private OperationResult UpdateUserPassword(UserAccount user, string previousPassword, string newPassword)
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
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }
}
