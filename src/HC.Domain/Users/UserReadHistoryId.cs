using System;

namespace HC.Domain.Users;

public sealed record UserReadHistoryId(Guid Value) : Identity(Value);
