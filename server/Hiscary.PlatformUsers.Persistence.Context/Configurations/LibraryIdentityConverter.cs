using Enterprise.Persistence.Context.Configurations;

namespace Hiscary.PlatformUsers.Persistence.Context.Configurations;

public class LibraryIdentityConverter : IdentityConverter<LibraryId>
{
    public LibraryIdentityConverter() :
        base((x) => new LibraryId(x))
    {
    }
}
