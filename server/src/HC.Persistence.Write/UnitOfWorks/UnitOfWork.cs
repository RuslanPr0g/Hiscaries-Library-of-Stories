using HC.Application.DataAccess;
using HC.Persistence.Write.DataAccess;
using System.Threading.Tasks;

namespace HC.Persistence.Write.UnitOfWorks;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly HiscaryContext _context;

    public UnitOfWork(HiscaryContext context)
    {
        _context = context;
    }

    public Task<int> SaveChanges() => _context.SaveChangesAsync();
}
