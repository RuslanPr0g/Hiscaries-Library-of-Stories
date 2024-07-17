namespace HC.Domain;

public abstract class Entity<T> where T : IIdentity
{
    public IIdentity Id { get; init; }

    protected Entity(IIdentity id)
    {
        Id = id;
    }

    // TODO: ADD EQUALS OVERRIDE, IEQUTABLE, AND OPERATORS ==, !=

    protected Entity()
    {
    }
}
