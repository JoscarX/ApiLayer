using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para registros de auditoría (solo lectura/escritura, nunca modificación).</summary>
public interface IAuditoriaRepository
{
    Task<IEnumerable<Auditoria>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Auditoria>> ObtenerPorTablaAsync(string tabla, CancellationToken ct = default);
    Task<IEnumerable<Auditoria>> ObtenerPorUsuarioAsync(int usuarioId, CancellationToken ct = default);
    Task<IEnumerable<Auditoria>> ObtenerPorAccionAsync(AccionAuditoria accion, CancellationToken ct = default);
    Task<IEnumerable<Auditoria>> ObtenerPorRangoFechaAsync(DateTime desde, DateTime hasta, CancellationToken ct = default);
    Task AgregarAsync(Auditoria auditoria, CancellationToken ct = default);
}
