namespace HC.Application.Write.Generators;

public interface IIdGenerator
{
    T Generate<T>(Func<Guid, T> generator) where T : Identity;
}
