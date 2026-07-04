using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para la gestión de roles del sistema.</summary>
public interface IRolRepository
{
    Task<Rol?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<Rol?> ObtenerPorNombreAsync(string nombre, CancellationToken ct = default);
    Task<IEnumerable<Rol>> ObtenerTodosAsync(CancellationToken ct = default);
    Task AgregarAsync(Rol rol, CancellationToken ct = default);
    void Actualizar(Rol rol);
    void Eliminar(Rol rol);
    Task<bool> ExistePorNombreAsync(string nombre, CancellationToken ct = default);
}
