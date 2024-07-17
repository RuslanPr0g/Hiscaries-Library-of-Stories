namespace HC.Domain;

public interface IIdentity
{
};

public record Identity<T>(T Value) : IIdentity where T : struct
{
    public static implicit operator Identity<T>(T identity) => new Identity<T>(identity);
    public static implicit operator T(Identity<T> identity) => identity.Value;
}
