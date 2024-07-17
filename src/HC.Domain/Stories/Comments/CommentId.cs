using System;

namespace HC.Domain.Stories.Comments;

public record CommentId(Guid Value) : Identity<Guid>(Value);
