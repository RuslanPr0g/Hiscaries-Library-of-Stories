using Enterprise.Persistence.Context.Configurations;

namespace HC.PlatformUsers.Persistence.Context.Configurations;

public class LibraryIdentityConverter : IdentityConverter<LibraryId>
{
    public LibraryIdentityConverter() :
        base((x) => new LibraryId(x))
    {
    }
}
