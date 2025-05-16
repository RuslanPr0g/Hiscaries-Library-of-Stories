using Enterprise.Domain;

public sealed record RefreshTokenId(Guid Value) : Identity(Value)
{
    public static implicit operator RefreshTokenId(Guid identity) => new(identity);
    public static implicit operator Guid(RefreshTokenId identity) => identity.Value;
}

