using HC.Application.Read.Users.DataAccess;
using HC.Application.Read.Users.ReadModels;
using HC.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Persistence.Read.Repositories;

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

