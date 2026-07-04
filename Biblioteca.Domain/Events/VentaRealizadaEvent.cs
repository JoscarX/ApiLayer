namespace Biblioteca.Domain.Events;

/// <summary>
/// Evento de dominio que se publica cuando se concreta una venta.
/// </summary>
public sealed record VentaRealizadaEvent(
    int VentaId,
    int VisitanteId,
    decimal Total,
    int CantidadItems,
    DateTime OcurrioEn) : IDomainEvent;
