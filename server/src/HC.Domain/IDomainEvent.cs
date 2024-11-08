using System;

namespace HC.Domain;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}