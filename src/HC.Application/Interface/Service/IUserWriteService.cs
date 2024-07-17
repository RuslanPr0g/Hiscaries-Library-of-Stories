using HC.Application.Models.Response;
using HC.Application.Users.Command;
using HC.Application.Users.Command.LoginUser;
using HC.Application.Users.Command.PublishReview;
using HC.Application.Users.Command.RefreshToken;
using HC.Domain.Users;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IUserWriteService
{
    Task<User> GetUserById(UserId userId);
    Task<User> GetUserByUsername(string username);
    Task BecomePublisher(string username);

    Task<RegisterUserResult> RegisterUser(RegisterUserCommand command);
    Task<LoginUserResult> LoginUser(LoginUserCommand command);

    Task<int> PublishReview(PublishReviewCommand command);
    Task DeleteReview(DeleteReviewCommand command);
    Task<UpdateUserDataResult> UpdateUserData(UpdateUserDataCommand command);
    Task<RefreshTokenResponse> RefreshToken(RefreshTokenCommand command);
}