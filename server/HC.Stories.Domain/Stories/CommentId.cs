﻿using Enterprise.Domain;

namespace HC.Stories.Domain.Stories;

public record CommentId(Guid Value) : Identity(Value)
{
    public static implicit operator CommentId(Guid identity) => new(identity);
    public static implicit operator Guid(CommentId identity) => identity.Value;
}
