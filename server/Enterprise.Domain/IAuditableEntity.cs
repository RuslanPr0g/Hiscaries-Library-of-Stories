namespace Enterprise.Domain;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    DateTime EditedAt { get; set; }
    int Version { get; set; }
}
