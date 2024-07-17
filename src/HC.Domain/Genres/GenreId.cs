using System;

namespace HC.Domain.Genres;

public record GenreId(Guid Value) : Identity<Guid>(Value);
