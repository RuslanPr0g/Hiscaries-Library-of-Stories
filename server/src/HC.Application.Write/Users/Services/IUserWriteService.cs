using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Command;
using HC.Application.Write.Users.Command;
using HC.Domain.Users;

namespace HC.Application.Write.Users.Services;

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