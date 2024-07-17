using HC.Domain;
using System;

public sealed record StoryBookMarkId(Guid Value) : Identity(Value);