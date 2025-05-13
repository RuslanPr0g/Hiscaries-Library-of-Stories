namespace Enterprise.Domain;

public interface IValueObject<T>
{
    T Value { get; }
}