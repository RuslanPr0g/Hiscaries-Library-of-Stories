using Enterprise.Domain;

public abstract class Entity : IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime EditedAt { get; set; }
    public int Version { get; set; }
}

public abstract class Entity<T> :
    Entity,
    IEquatable<Entity<T>> where T : Identity
{
    public T Id { get; init; }

    protected Entity(T id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<T> other)
            return false;

        return Equals(other);
    }

    public bool Equals(Entity<T>? other)
    {
        if (other is null)
            return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<T>? left, Entity<T>? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity<T>? left, Entity<T>? right)
    {
        return !(left == right);
    }

    protected Entity()
    {
    }
}