namespace Biblioteca.Domain.Events;

/// <summary>
/// Evento de dominio que se publica cuando un libro es prestado.
/// Permite desacoplar reacciones (notificaciones, métricas) del caso de uso principal.
/// </summary>
public sealed record LibroPrestadoEvent(
    int LibroId,
    string TituloLibro,
    int VisitanteId,
    int PrestamoId,
    DateTime FechaEntrega,
    DateTime OcurrioEn) : IDomainEvent;
