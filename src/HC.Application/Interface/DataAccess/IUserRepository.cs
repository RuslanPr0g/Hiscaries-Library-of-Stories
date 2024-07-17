using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Domain.User;

namespace HC.Application.Interface;

public interface IUserRepository
{
    Task<RefreshToken> GetRefreshToken(string refreshToken);

    Task<List<User>> GetUsers();

    Task<List<Review>> GetReviews();
    Task InsertReview(Review review);
    Task DeleteReview(Review review);

    Task<User> GetUserById(int userId);

    Task<User> GetUserByUsername(string username);

    Task<string> GetUserRoleByUsername(string username);

    Task BecomePublisher(string username);

    Task<int> AddUser(User user);

    Task InsertRefreshToken(RefreshToken refreshToken);

    Task UpdateUserData(User user);

    Task UpdateRefreshToken(RefreshToken refreshToken);

    Task AddUserRoleLogin(User user);
}