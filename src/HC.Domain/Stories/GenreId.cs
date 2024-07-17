using System;

namespace HC.Domain.Stories;

public record GenreId(Guid Value) : Identity(Value);
