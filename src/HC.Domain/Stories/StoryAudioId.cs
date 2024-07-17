using System;

namespace HC.Domain.Stories;

public record StoryAudioId(Guid Value) : Identity<Guid>(Value);