﻿using StackNucleus.DDD.Domain;

namespace Hiscary.Stories.Domain.Stories;

public record StoryId(Guid Value) : Identity(Value)
{
    public static implicit operator StoryId(Guid identity) => new(identity);
    public static implicit operator Guid(StoryId identity) => identity.Value;
}
