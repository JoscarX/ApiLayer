using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para la gestión de ventas.</summary>
public interface IVentaRepository
{
    Task<Venta?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<Venta?> ObtenerPorIdConDetallesAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Venta>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Venta>> ObtenerPorVisitanteAsync(int visitanteId, CancellationToken ct = default);
    Task<IEnumerable<Venta>> ObtenerConDetallesAsync(CancellationToken ct = default);
    Task AgregarAsync(Venta venta, CancellationToken ct = default);
    void Actualizar(Venta venta);
}
