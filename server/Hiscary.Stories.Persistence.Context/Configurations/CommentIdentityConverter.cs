using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;
using Hiscary.Stories.Domain.Stories;

namespace Hiscary.Stories.Persistence.Context.Configurations;

public class CommentIdentityConverter : IdentityConverter<CommentId>
{
    public CommentIdentityConverter() :
        base((x) => new CommentId(x))
    {
    }
}
