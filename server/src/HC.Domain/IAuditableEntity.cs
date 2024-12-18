﻿using System;

namespace HC.Domain;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime EditedAt { get; set; }
    int Version { get; set; }
}
