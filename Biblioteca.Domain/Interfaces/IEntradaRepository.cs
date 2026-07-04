using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para el registro de entradas y salidas de visitantes.</summary>
public interface IEntradaRepository
{
    Task<Entrada?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Entrada>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Entrada>> ObtenerActivasAsync(CancellationToken ct = default);
    Task<IEnumerable<Entrada>> ObtenerPorVisitanteAsync(int visitanteId, CancellationToken ct = default);
    Task<Entrada?> ObtenerEntradaActivaPorVisitanteAsync(int visitanteId, CancellationToken ct = default);
    Task AgregarAsync(Entrada entrada, CancellationToken ct = default);
    void Actualizar(Entrada entrada);
}
