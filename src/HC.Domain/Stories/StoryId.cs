using System;

namespace HC.Domain.Stories;

public record StoryId(Guid Value) : Identity<Guid>(Value);
