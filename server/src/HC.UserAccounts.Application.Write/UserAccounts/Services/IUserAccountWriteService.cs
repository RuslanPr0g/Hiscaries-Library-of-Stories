using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.UserAccounts.Command.CreateUser;
using HC.Application.Write.UserAccounts.Command.LoginUser;
using HC.Application.Write.UserAccounts.Command.RefreshToken;
using HC.Application.Write.UserAccounts.Command.UpdateUserData;
using HC.Domain.UserAccounts;

namespace HC.Application.Write.Users.Services;

public interface IUserAccountWriteService
{
    Task<OperationResult> BanUser(UserAccountId userId);

    Task<OperationResult<TokenMetadata>> RegisterUser(RegisterUserCommand command);
    Task<OperationResult<TokenMetadata>> LoginUser(LoginUserCommand command);

    Task<OperationResult> UpdateUserData(UpdateUserDataCommand command);
    Task<OperationResult<TokenMetadata>> RefreshToken(RefreshTokenCommand command);
}