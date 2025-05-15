using Enterprise.Domain;

namespace Enterprise.Domain.Generators;

public interface IIdGenerator
{
    T Generate<T>(Func<Guid, T> generator) where T : Identity;
}
