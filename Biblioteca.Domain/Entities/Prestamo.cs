using Biblioteca.Domain.Common;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Events;
using Biblioteca.Domain.Exceptions;

namespace Biblioteca.Domain.Entities;

/// <summary>
/// Representa un préstamo de libro a un visitante.
/// Contiene toda la lógica de transición de estados (Activo → Devuelto/Vencido).
/// </summary>
public sealed class Prestamo : AuditableEntity
{
    public int VisitanteId { get; private set; }
    public int LibroId { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime FechaEntrega { get; private set; }
    public DateTime? FechaDevolucion { get; private set; }
    public EstadoPrestamo Estado { get; private set; }

    // Navegación
    public Visitante? Visitante { get; private set; }
    public Libro? Libro { get; private set; }

    private Prestamo() { }

    /// <summary>
    /// Crea un nuevo préstamo activo y aplica la lógica de disponibilidad en el libro.
    /// El libro debe llamar a Prestar() antes de crear el préstamo.
    /// </summary>
    public static Prestamo Crear(
        int visitanteId,
        int libroId,
        DateTime fechaInicio,
        DateTime fechaEntrega)
    {
        if (visitanteId <= 0)
            throw new DomainException("El visitante del préstamo no es válido.");

        if (libroId <= 0)
            throw new DomainException("El libro del préstamo no es válido.");

        if (fechaEntrega <= fechaInicio)
            throw new DomainException("La fecha de entrega debe ser posterior a la fecha de inicio.");

        return new Prestamo
        {
            VisitanteId = visitanteId,
            LibroId = libroId,
            FechaInicio = fechaInicio,
            FechaEntrega = fechaEntrega,
            Estado = EstadoPrestamo.Activo
        };
    }

    /// <summary>
    /// Marca el préstamo como devuelto y registra la fecha de devolución.
    /// </summary>
    /// <param name="fechaDevolucion">Fecha efectiva de la devolución.</param>
    /// <exception cref="DomainException">Si el préstamo ya fue devuelto.</exception>
    public void MarcarComoDevuelto(DateTime fechaDevolucion)
    {
        if (Estado == EstadoPrestamo.Devuelto)
            throw new DomainException($"El préstamo #{Id} ya fue marcado como devuelto.");

        FechaDevolucion = fechaDevolucion;
        Estado = EstadoPrestamo.Devuelto;
    }

    /// <summary>
    /// Marca el préstamo como vencido cuando supera la fecha de entrega sin devolverse.
    /// </summary>
    /// <exception cref="DomainException">Si el préstamo ya está en estado final.</exception>
    public void MarcarComoVencido()
    {
        if (Estado != EstadoPrestamo.Activo)
            throw new DomainException($"Solo se pueden vencer préstamos activos. Estado actual: {Estado}.");

        Estado = EstadoPrestamo.Vencido;
    }

    /// <summary>Indica si el préstamo está activo.</summary>
    public bool EstaActivo => Estado == EstadoPrestamo.Activo;

    /// <summary>Indica si el préstamo está vencido (no devuelto a tiempo).</summary>
    public bool EstaVencido(DateTime fechaActual) =>
        Estado == EstadoPrestamo.Activo && fechaActual > FechaEntrega;
}
