using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Enterprise.Persistence.Context.Configurations;

public class IdentityConverter<TIdentity> : ValueConverter<TIdentity, Guid>
    where TIdentity : Identity
{
    public IdentityConverter(Func<Guid, TIdentity> generator) : base(
        identity => identity.Value,
        value => generator.Invoke(value))
    {
    }
}
