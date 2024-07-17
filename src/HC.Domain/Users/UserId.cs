using System;

namespace HC.Domain.Users;

public sealed record UserId(Guid Value) : Identity(Value);
