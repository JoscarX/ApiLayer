using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para la gestión de préstamos de libros.</summary>
public interface IPrestamoRepository
{
    Task<Prestamo?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Prestamo>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Prestamo>> ObtenerPorVisitanteAsync(int visitanteId, CancellationToken ct = default);
    Task<IEnumerable<Prestamo>> ObtenerPorLibroAsync(int libroId, CancellationToken ct = default);
    Task<IEnumerable<Prestamo>> ObtenerPorEstadoAsync(EstadoPrestamo estado, CancellationToken ct = default);
    Task<IEnumerable<Prestamo>> ObtenerVencidosAsync(DateTime fechaActual, CancellationToken ct = default);
    Task<IEnumerable<Prestamo>> ObtenerActivosAsync(CancellationToken ct = default);
    Task<bool> TienePrestamoActivoPorLibroAsync(int visitanteId, int libroId, CancellationToken ct = default);
    Task AgregarAsync(Prestamo prestamo, CancellationToken ct = default);
    void Actualizar(Prestamo prestamo);
}
