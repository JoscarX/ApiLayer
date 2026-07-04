namespace Biblioteca.Domain.Events;

/// <summary>
/// Contrato base para todos los eventos de dominio.
/// Permite publicar y procesar eventos de forma desacoplada.
/// </summary>
public interface IDomainEvent
{
    /// <summary>Momento exacto en que ocurrió el evento.</summary>
    DateTime OcurrioEn { get; }
}
