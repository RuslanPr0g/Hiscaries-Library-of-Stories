using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IUserReadRepository
{
    Task<List<UserReadModel>> GetUsers();
    Task<UserReadModel> GetUserById(Guid userId);
    Task<UserReadModel> GetUserByUsername(string username);
}