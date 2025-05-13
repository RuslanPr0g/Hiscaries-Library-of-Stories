namespace HC.Persistence.Context.Configurations.Converters;

public class ReviewIdentityConverter : IdentityConverter<ReviewId>
{
    public ReviewIdentityConverter() :
        base((x) => new ReviewId(x))
    {
    }
}
