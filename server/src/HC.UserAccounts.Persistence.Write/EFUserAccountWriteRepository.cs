using Microsoft.EntityFrameworkCore;

namespace HC.UserAccounts.Persistence.Write;

public class EFUserAccountWriteRepository : IUserAccountWriteRepository
{
    private readonly HiscaryContext _context;

    public EFUserAccountWriteRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<UserAccount?> GetById(UserAccountId userId) =>
        await _context.UserAccounts
            .FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<UserAccount?> GetByUsername(string username) =>
        await _context.UserAccounts
            .FirstOrDefaultAsync(x => x.Username == username);

    public async Task<bool> IsExistByEmail(string email) =>
        await _context.UserAccounts.AnyAsync(x => x.Email == email);

    public async Task Add(UserAccount user) =>
        await _context.UserAccounts.AddAsync(user);
}

