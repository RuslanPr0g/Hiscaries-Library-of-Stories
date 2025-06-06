﻿using Enterprise.Domain;

namespace HC.Stories.Domain.Stories;

public record StoryId(Guid Value) : Identity(Value)
{
    public static implicit operator StoryId(Guid identity) => new(identity);
    public static implicit operator Guid(StoryId identity) => identity.Value;
}
