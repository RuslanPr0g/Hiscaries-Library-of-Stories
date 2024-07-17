using HC.Domain;
using System;

public sealed record ReviewId(Guid Value) : Identity(Value);
