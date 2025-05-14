namespace HC.UserAccounts.Domain.Services;

public interface IUserAccountWriteService
{
    Task<OperationResult> BanUser(UserAccountId userId);

    Task<OperationResult<TokenMetadata>> RegisterUser(RegisterUserCommand command);
    Task<OperationResult<TokenMetadata>> LoginUser(LoginUserCommand command);

    Task<OperationResult> UpdateUserData(UpdateUserDataCommand command);
    Task<OperationResult<TokenMetadata>> RefreshToken(RefreshTokenCommand command);
}