﻿using HC.Domain;
using System;

public sealed record PlatformUserId(Guid Value) : Identity(Value)
{
    public static implicit operator PlatformUserId(Guid identity) => new(identity);
    public static implicit operator Guid(PlatformUserId identity) => identity.Value;
}
