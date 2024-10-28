using HC.Domain.Stories;

namespace HC.Persistence.Write.Configurations.Converters;

public class CommentIdentityConverter : IdentityConverter<CommentId>
{
    public CommentIdentityConverter() : 
        base((x) => new CommentId(x))
    {
    }
}
