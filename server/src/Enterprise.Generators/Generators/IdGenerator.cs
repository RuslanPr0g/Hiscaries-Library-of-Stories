namespace Enterprise.Generators.Generators;

public class IdGenerator : IIdGenerator
{
    public T Generate<T>(Func<Guid, T> generator) where T : Identity =>
        generator.Invoke(Guid.NewGuid());
}
