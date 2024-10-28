﻿using HC.Domain;
using System;

namespace HC.Application.Write.Generators;

public class IdGenerator : IIdGenerator
{
    public T Generate<T>(Func<Guid, T> generator) where T : Identity =>
        generator.Invoke(Guid.NewGuid());
}