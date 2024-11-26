﻿using System;

namespace HC.Domain.PlatformUsers.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class UserSubscribedToLibraryDomainEvent(Guid PlatformUserId, Guid LibraryId) : IDomainEvent
{
    public Guid PlatformUserId { get; } = PlatformUserId;
    public Guid LibraryId { get; } = LibraryId;
}