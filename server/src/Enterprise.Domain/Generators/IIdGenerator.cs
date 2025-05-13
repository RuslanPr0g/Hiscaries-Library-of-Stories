using Enterprise.Domain;

namespace Enterprise.Generators;

public interface IIdGenerator
{
    T Generate<T>(Func<Guid, T> generator) where T : Identity;
}
