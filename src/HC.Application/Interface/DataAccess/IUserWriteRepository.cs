﻿using HC.Domain.Users;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IUserWriteRepository
{
    Task<User> GetUserById(UserId userId);
    Task<User> GetUserByUsername(string username);
    Task<string> GetUserRoleByUsername(string username);
    Task<int> AddUserAndRole(User user);
    Task BecomePublisher(string username);
    Task InsertRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken> GetRefreshToken(string refreshToken);
    Task DeleteReview(Review review);
}