using HC.Application.Users.DataAccess;
using HC.Application.Users.ReadModels;
using HC.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HC.Infrastructure.Repository;

public class EFUserReadRepository : IUserReadRepository
{
    private readonly HiscaryContext _context;

    public EFUserReadRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<UserAccountOwnerReadModel?> GetUserById(Guid userId) =>
        await _context.Users
            .AsNoTracking()
            .Where(x => x.Id.Value == userId)
            .Select(user => UserAccountOwnerReadModel.FromDomainModel(user))
            .FirstOrDefaultAsync();

    public async Task<UserSimpleReadModel?> GetUserByUsername(string username) =>
        await _context.Users
            .AsNoTracking()
            .Where(x => x.Username == username)
            .Select(user => UserSimpleReadModel.FromDomainModel(user))
            .FirstOrDefaultAsync();
}

