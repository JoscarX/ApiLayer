namespace Biblioteca.Domain.Events;

/// <summary>
/// Evento de dominio que se publica cuando un libro es devuelto.
/// </summary>
public sealed record LibroDevueltoEvent(
    int LibroId,
    string TituloLibro,
    int VisitanteId,
    int PrestamoId,
    DateTime FechaDevolucion,
    DateTime OcurrioEn) : IDomainEvent;
