using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;

namespace Hiscary.PlatformUsers.Persistence.Context.Configurations;

public class LibraryIdentityConverter : IdentityConverter<LibraryId>
{
    public LibraryIdentityConverter() :
        base((x) => new LibraryId(x))
    {
    }
}
