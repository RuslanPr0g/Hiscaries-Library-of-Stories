public sealed record LibraryId(Guid Value) : Identity(Value)
{
    public static implicit operator LibraryId(Guid identity) => new(identity);
    public static implicit operator Guid(LibraryId identity) => identity.Value;
}
