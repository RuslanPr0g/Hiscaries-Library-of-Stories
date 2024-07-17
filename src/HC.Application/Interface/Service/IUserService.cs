using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Application.Models.Response;
using HC.Domain.User;

namespace HC.Application.Interface;

public interface IUserService
{
    Task<RegisterUserResult> RegisterUser(User user);
    Task DeleteReview(Review review);
    Task<LoginUserResult> LoginUser(string username, string password);
    Task<int> PublishReview(Review review);
    Task<UpdateUserDataResult> UpdateUserData(User user);
    Task<List<Review>> GetAllReviews();
    Task<(int tr, int ts, double asc)> GetAdvancedInfoByUsername(string username);
    Task<RefreshTokenResponse> RefreshToken(string token, string refreshToken);
    Task<User> GetUserById(int userId);
    Task<User> GetUserByUsername(string username);
    Task<string> GetUserRoleByUsername(string username);
    Task BecomePublisher(string username);
    Task<IList<User>> GetAllUsers();
}