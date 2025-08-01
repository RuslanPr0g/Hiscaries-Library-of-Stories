﻿using StackNucleus.DDD.Domain.Events;

namespace Hiscary.Media.IntegrationEvents.Outgoing;

public sealed class ImageUploadedIntegrationEvent(
    Guid RequesterId,
    string ImageUrl
    ) : BaseIntegrationEvent
{
    public Guid RequesterId { get; set; } = RequesterId;
    public string ImageUrl { get; set; } = ImageUrl;
}
