using HC.Application.Write.PlatformUsers.Command.DeleteReview;
using HC.Application.Write.PlatformUsers.Command.PublishReview;
using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Command;
using HC.Application.Write.UserAccounts.Command.CreateUser;
using HC.Application.Write.UserAccounts.Command.LoginUser;
using HC.Application.Write.UserAccounts.Command.RefreshToken;
using HC.Application.Write.UserAccounts.Command.UpdateUserData;
using HC.Domain.Users;

namespace HC.Application.Write.PlatformUsers.Services;

public interface IPlatformUserWriteService
{
    Task<OperationResult<User>> GetUserById(UserId userId);
    Task<OperationResult<User>> GetUserByUsername(string username);
    Task<OperationResult> BecomePublisher(string username);

    Task<OperationResult> BanUser(string username);

    Task<OperationResult<UserWithTokenResponse>> RegisterUser(RegisterUserCommand command);
    Task<OperationResult<UserWithTokenResponse>> LoginUser(LoginUserCommand command);

    Task<OperationResult> PublishReview(PublishReviewCommand command);
    Task<OperationResult> DeleteReview(DeleteReviewCommand command);
    Task<OperationResult> UpdateUserData(UpdateUserDataCommand command);
    Task<OperationResult<UserWithTokenResponse>> RefreshToken(RefreshTokenCommand command);

    Task<OperationResult> BookmarkStory(BookmarkStoryCommand command);
    Task<OperationResult> ReadStoryHistory(ReadStoryCommand command);
}