﻿using HC.Domain.Stories;

namespace HC.Persistence.Context.Configurations.Converters;

public class GenreIdentityConverter : IdentityConverter<GenreId>
{
    public GenreIdentityConverter() :
        base((x) => new GenreId(x))
    {
    }
}