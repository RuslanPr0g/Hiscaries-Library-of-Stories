namespace HC.Stories.Persistence.Context.Configurations;

public class GenreIdentityConverter : IdentityConverter<GenreId>
{
    public GenreIdentityConverter() :
        base((x) => new GenreId(x))
    {
    }
}
