using HC.UserAccounts.Domain;
using HC.UserAccounts.Domain.DataAccess;
using HC.UserAccounts.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.UserAccounts.Persistence.Write;

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
