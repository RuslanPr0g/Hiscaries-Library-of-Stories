using System;

namespace HC.Domain.Genres;

public record GenreId(Guid Value) : Identity(Value)
{
    public static implicit operator GenreId(Guid identity) => new(identity);
    public static implicit operator Guid(GenreId identity) => identity.Value;
}
