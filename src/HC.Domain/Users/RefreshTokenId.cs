using HC.Domain;
using System;

public sealed record RefreshTokenId(Guid Value) : Identity(Value);
