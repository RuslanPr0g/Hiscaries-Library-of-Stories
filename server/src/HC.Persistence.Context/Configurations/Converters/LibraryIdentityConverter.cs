namespace HC.Persistence.Context.Configurations.Converters;

public class LibraryIdentityConverter : IdentityConverter<LibraryId>
{
    public LibraryIdentityConverter() :
        base((x) => new LibraryId(x))
    {
    }
}
