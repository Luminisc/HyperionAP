namespace HyperionAP.Core.Models.EntitiesBase;

public abstract class Entity(Guid id)
{
    public Entity() : this(Guid.Empty) { }

    public Guid Id { get; protected set; } = id;

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
}
