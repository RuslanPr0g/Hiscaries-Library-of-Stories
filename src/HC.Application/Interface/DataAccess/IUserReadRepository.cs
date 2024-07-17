using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IUserReadRepository
{
    // USE DAPPER HERE TO RETURN THE READ MODELS AS VIEWS, ETC.
    Task<List<UserReadModel>> GetUsers();
    Task<UserReadModel> GetUserById(int userId);
    Task<UserReadModel> GetUserByUsername(string username);
    Task<string> GetUserRoleByUsername(string username);
    Task<List<UserReadHistoryReadModel>> GetUserReadHistory();
}