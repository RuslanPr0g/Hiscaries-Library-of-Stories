using System;

namespace HC.Domain.Stories;

public sealed record StoryRatingId(Guid Value) : Identity(Value);
