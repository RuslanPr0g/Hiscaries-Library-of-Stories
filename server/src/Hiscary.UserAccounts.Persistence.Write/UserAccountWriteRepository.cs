using Hiscary.UserAccounts.Domain;
using Hiscary.UserAccounts.Domain.DataAccess;
using Hiscary.UserAccounts.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using StackNucleus.DDD.Persistence.EF.Postgres;

namespace Hiscary.UserAccounts.Persistence.Write;

public class UserAccountWriteRepository(UserAccountsContext context)
    : BaseWriteRepository<UserAccount, UserAccountId, UserAccountsContext>,
    IUserAccountWriteRepository
{
    protected override UserAccountsContext Context { get; init; } = context;

    public async Task<UserAccount?> GetByUsername(string username) =>
        await Context.UserAccounts
            .FirstOrDefaultAsync(x => x.Username == username);
    public async Task<bool> IsExistByEmail(string email) =>
        await Context.UserAccounts
            .AnyAsync(x => x.Email == email);
}
