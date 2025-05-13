using HC.UserAccounts.Application.Write.UserAccounts.Command.LoginUser;
using HC.UserAccounts.Application.Write.UserAccounts.Command.RefreshToken;
using HC.UserAccounts.Application.Write.UserAccounts.Command.RegisterUser;
using HC.UserAccounts.Application.Write.UserAccounts.Command.UpdateUserData;
using HC.UserAccounts.Application.Write.UserAccounts.DataAccess;

namespace HC.UserAccounts.Application.Write.UserAccounts.Services;

public sealed class UserAccountWriteService : IUserAccountWriteService
{
    private readonly IUserAccountWriteRepository _repository;
    private readonly IIdGenerator _idGenerator;
    private readonly JwtSettings _jwtSettings;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IJWTTokenHandler _tokenHandler;
    private readonly ILogger<UserAccountWriteService> _logger;
    private readonly SaltSettings _saltSettings;

    public UserAccountWriteService(
        IUserAccountWriteRepository repository,
        JwtSettings jwtSettings,
        TokenValidationParameters tokenValidationParameters,
        IIdGenerator idGenerator,
        IJWTTokenHandler tokenHandler,
        ILogger<UserAccountWriteService> logger,
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

    public async Task<OperationResult> BanUser(UserAccountId userId)
    {
        UserAccount? user = await _repository.GetById(userId);

        if (user is null)
        {
            _logger.LogWarning("User {id} was not found to be banned", userId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UserIsNotFound);
        }

        user.Ban();

        _logger.LogInformation("User {id} is now banned from using the system", userId);
        return OperationResult.CreateSuccess();
    }

    public async Task<OperationResult<TokenMetadata>> LoginUser(LoginUserCommand command)
    {
        _logger.LogInformation("Attempting to log in user {Username}", command.Username);
        UserAccount? user = await _repository.GetByUsername(command.Username);

        if (user is null)
        {
            _logger.LogWarning("Login attempt failed: User {Username} not found", command.Username);
            return OperationResult<TokenMetadata>.CreateNotFound(UserFriendlyMessages.UserIsNotFound);
        }

        var passwordMatch = HashPassword(command.Password, _saltSettings.StoredSalt) == user.Password;

        if (passwordMatch is false)
        {
            _logger.LogWarning("Login attempt failed: Password mismatch for user {Username}", command.Username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.PasswordMismatch);
        }

        if (user.IsBanned)
        {
            _logger.LogWarning("Login attempt failed: User {Username} is banned", command.Username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.UserIsBanned);
        }

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(user, _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate JWT token for user {Username}", command.Username);
            return OperationResult<TokenMetadata>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        user.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        _logger.LogInformation("User {Username} successfully logged in", command.Username);
        return OperationResult<TokenMetadata>.CreateSuccess(new TokenMetadata
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult<TokenMetadata>> RegisterUser(RegisterUserCommand command)
    {
        _logger.LogInformation("Attempting to register new user with username {Username}", command.Username);

        UserAccount? existingUser = await _repository.GetByUsername(command.Username);

        if (existingUser is not null)
        {
            _logger.LogWarning("Registration failed: Username {Username} already exists", command.Username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.UserWithUsernameExists);
        }

        var exists = await _repository.IsExistByEmail(command.Email);

        if (exists)
        {
            _logger.LogWarning("Registration failed: Email {Email} already in use", command.Email);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.UserWithEmailExists);
        }

        string encryptedpassword = HashPassword(command.Password, _saltSettings.StoredSalt);

        var userId = _idGenerator.Generate((id) => new UserAccountId(id));
        var createdUser = new UserAccount(
            userId,
            command.Username,
            command.Email,
            encryptedpassword,
            command.BirthDate
            );

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(createdUser, _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate JWT token for new user {Username}", command.Username);
            return OperationResult<TokenMetadata>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        createdUser.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        await _repository.Add(createdUser);

        _logger.LogInformation("Successfully registered new user {Username} with ID {UserId}", command.Username, userId);
        return OperationResult<TokenMetadata>.CreateSuccess(new TokenMetadata
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult<TokenMetadata>> RefreshToken(RefreshTokenCommand command)
    {
        _logger.LogInformation("Attempting to refresh token for user {Username}", command.Username);
        UserAccount? user = await _repository.GetByUsername(command.Username);

        if (user is null)
        {
            _logger.LogWarning("Token refresh failed: User {Username} not found", command.Username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.UserIsNotFound);
        }

        var validatedToken = _tokenHandler.GetTokenWithClaims(command.Token, _tokenValidationParameters);

        if (validatedToken is null)
        {
            _logger.LogWarning("Token refresh failed: Invalid token for user {Username}", command.Username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.PleaseRelogin);
        }

        if (validatedToken.ExpiryDate > DateTime.UtcNow)
        {
            _logger.LogWarning("Token refresh failed: Token not expired for user {Username}", command.Username);
            return OperationResult<TokenMetadata>.CreateValidationsError(UserFriendlyMessages.RefreshTokenIsNotExpired);
        }

        bool validated = user.ValidateRefreshToken(validatedToken.JTI);

        if (!validated)
        {
            _logger.LogWarning("Token refresh failed: Invalid refresh token for user {Username}", command.Username);
            return OperationResult<TokenMetadata>.CreateClientSideError(UserFriendlyMessages.RefreshTokenIsExpired);
        }

        (string generatedToken, RefreshTokenDescriptor generatedRefreshToken) =
            await _tokenHandler.GenerateJwtToken(user, _jwtSettings);

        if (string.IsNullOrEmpty(generatedToken) || string.IsNullOrEmpty(generatedRefreshToken.Token))
        {
            _logger.LogError("Failed to generate new JWT token for user {Username}", command.Username);
            return OperationResult<TokenMetadata>.CreateServerSideError(UserFriendlyMessages.TryAgainLater);
        }

        user.UpdateRefreshToken(
            generatedRefreshToken.ToDomainModel(
                _idGenerator.Generate((id) => new RefreshTokenId(id))));

        _logger.LogInformation("Successfully refreshed token for user {Username}", command.Username);
        return OperationResult<TokenMetadata>.CreateSuccess(new TokenMetadata
        {
            Token = generatedToken,
            RefreshToken = generatedRefreshToken.Token
        });
    }

    public async Task<OperationResult> UpdateUserData(UpdateUserDataCommand command)
    {
        _logger.LogInformation("Attempting to update user data for {Username}", command.Username);
        UserAccount? user = await _repository.GetById(command.Id);

        if (user is null)
        {
            _logger.LogWarning("Update user data failed: User {Username} not found", command.Username);
            return OperationResult.CreateValidationsError(UserFriendlyMessages.UserIsNotFound);
        }

        if (!string.IsNullOrEmpty(command.Username))
        {
            _logger.LogInformation("Checking if new username {Username} is available", command.Username);
            var newUsernameUser = await _repository.GetByUsername(command.Username);

            if (newUsernameUser is not null)
            {
                _logger.LogWarning("Update user data failed: New username {Username} already exists", command.Username);
                return OperationResult.CreateValidationsError(UserFriendlyMessages.UserWithUsernameExists);
            }
        }

        var existsWithEmail = await _repository.IsExistByEmail(command.Email);

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
                user.UpdatePersonalInformation(command.Username, command.Email, command.BirthDate);
            }
            else
            {
                _logger.LogWarning("Password update failed for user {Username}", command.Username);
            }

            return result;
        }
        else
        {
            user.UpdatePersonalInformation(command.Username, command.Email, command.BirthDate);
        }

        _logger.LogInformation("Successfully updated user data for {Username}", command.Username);
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
        return BCrypt.Net.BCrypt.HashPassword(password, _saltSettings.StoredSalt);
    }
}
