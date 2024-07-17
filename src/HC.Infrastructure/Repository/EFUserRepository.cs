using HC.Infrastructure.DataAccess;
using HC.Application.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Domain.User;

namespace HC.Infrastructure.Repository;
public class EFUserRepository : IUserRepository
{
    private readonly HiscaryContext _context;

    public EFUserRepository(HiscaryContext context)
    {
        _context = context;
    }

    public Task<RefreshToken> GetRefreshToken(string refreshToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<User>> GetUsers()
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Review>> GetReviews()
    {
        throw new System.NotImplementedException();
    }

    public Task InsertReview(Review review)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteReview(Review review)
    {
        throw new System.NotImplementedException();
    }

    public Task<User> GetUserById(int userId)
    {
        throw new System.NotImplementedException();
    }

    public Task<User> GetUserByUsername(string username)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> GetUserRoleByUsername(string username)
    {
        throw new System.NotImplementedException();
    }

    public Task BecomePublisher(string username)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> AddUser(User user)
    {
        throw new System.NotImplementedException();
    }

    public Task InsertRefreshToken(RefreshToken refreshToken)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateUserData(User user)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateRefreshToken(RefreshToken refreshToken)
    {
        throw new System.NotImplementedException();
    }

    public Task AddUserRoleLogin(User user)
    {
        throw new System.NotImplementedException();
    }
}

