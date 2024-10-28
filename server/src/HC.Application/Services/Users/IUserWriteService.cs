using HC.Application.Models.Response;
using HC.Application.Stories.Command.ReadStory;
using HC.Application.Stories.Command;
using HC.Application.Users.Command;
using HC.Application.Users.Command.LoginUser;
using HC.Application.Users.Command.PublishReview;
using HC.Application.Users.Command.RefreshToken;
using HC.Domain.Users;
using System.Threading.Tasks;
using HC.Application.ResultModels.Response;

namespace HC.Application.Services;

public interface IUserWriteService
{
    Task<OperationResult<User>> GetUserById(UserId userId);
    Task<OperationResult<User>> GetUserByUsername(string username);
    Task<OperationResult> BecomePublisher(string username);

    Task<OperationResult<UserWithTokenResponse>> RegisterUser(RegisterUserCommand command);
    Task<OperationResult<UserWithTokenResponse>> LoginUser(LoginUserCommand command);

    Task<OperationResult> PublishReview(PublishReviewCommand command);
    Task<OperationResult> DeleteReview(DeleteReviewCommand command);
    Task<OperationResult> UpdateUserData(UpdateUserDataCommand command);
    Task<OperationResult<UserWithTokenResponse>> RefreshToken(RefreshTokenCommand command);

    Task<OperationResult> BookmarkStory(BookmarkStoryCommand command);
    Task<OperationResult> ReadStoryHistory(ReadStoryCommand command);
}