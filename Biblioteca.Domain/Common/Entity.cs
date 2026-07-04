using Biblioteca.Domain.Events;

namespace Biblioteca.Domain.Common;

/// <summary>
/// Clase base para todas las entidades del dominio.
/// Proporciona identidad, comparación por valor de Id y soporte para Domain Events.
/// </summary>
public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>Identificador único de la entidad.</summary>
    public int Id { get; private set; }

    /// <summary>Colección de eventos de dominio pendientes de publicar.</summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>Registra un evento de dominio para su publicación posterior.</summary>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>Limpia todos los eventos de dominio registrados.</summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        if (Id == 0 || other.Id == 0) return false;
        return Id == other.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Entity? left, Entity? right) =>
        left is null ? right is null : left.Equals(right);

    public static bool operator !=(Entity? left, Entity? right) => !(left == right);
}
