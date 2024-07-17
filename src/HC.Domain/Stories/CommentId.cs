using System;

namespace HC.Domain.Stories;

public record CommentId(Guid Value) : Identity(Value);
