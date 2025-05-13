using HC.UserAccounts.Application.Write.UserAccounts.Command.LoginUser;
using HC.UserAccounts.Application.Write.UserAccounts.Command.RefreshToken;
using HC.UserAccounts.Application.Write.UserAccounts.Command.RegisterUser;
using HC.UserAccounts.Application.Write.UserAccounts.Command.UpdateUserData;

namespace HC.UserAccounts.Application.Write.UserAccounts.Services;

public interface IUserAccountWriteService
{
    Task<OperationResult> BanUser(UserAccountId userId);

    Task<OperationResult<TokenMetadata>> RegisterUser(RegisterUserCommand command);
    Task<OperationResult<TokenMetadata>> LoginUser(LoginUserCommand command);

    Task<OperationResult> UpdateUserData(UpdateUserDataCommand command);
    Task<OperationResult<TokenMetadata>> RefreshToken(RefreshTokenCommand command);
}