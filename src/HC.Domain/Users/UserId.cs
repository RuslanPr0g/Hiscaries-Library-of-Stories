using System;

namespace HC.Domain.Users;

public record UserId(Guid Value) : Identity<Guid>(Value);
