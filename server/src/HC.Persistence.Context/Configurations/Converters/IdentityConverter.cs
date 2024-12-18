﻿using HC.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HC.Persistence.Context.Configurations.Converters;

public class IdentityConverter<TIdentity> : ValueConverter<TIdentity, Guid>
    where TIdentity : Identity
{
    public IdentityConverter(Func<Guid, TIdentity> generator) : base(
        identity => identity.Value,
        value => generator.Invoke(value))
    {
    }
}
