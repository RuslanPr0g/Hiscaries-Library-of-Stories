using Enterprise.Persistence.Context.Configurations;
using HC.Stories.Domain.Stories;

namespace HC.Stories.Persistence.Context.Configurations;

public class CommentIdentityConverter : IdentityConverter<CommentId>
{
    public CommentIdentityConverter() :
        base((x) => new CommentId(x))
    {
    }
}
