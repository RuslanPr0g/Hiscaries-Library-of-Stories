using Hiscary.Domain.ResultModels.Response;

namespace Hiscary.UserAccounts.Domain.Services;

public interface IUserAccountWriteService
{
    Task<OperationResult> BanUser(UserAccountId userId);

    Task<OperationResult<TokenMetadata>> RegisterUser(
        string username,
        string email,
        string password,
        DateTime birthDate);

    Task<OperationResult<TokenMetadata>> LoginUser(
        string username,
        string password);

    Task<OperationResult> UpdateUserData(
        Guid id,
        string username,
        string email,
        DateTime birthDate,
        string previousPassword,
        string newPassword);

    Task<OperationResult<TokenMetadata>> RefreshToken(
        string username,
        string token,
        string refreshToken);
}