using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>
/// Repositorio para la gestión de usuarios del sistema.
/// </summary>
public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<Usuario?> ObtenerPorCedulaAsync(string cedula, CancellationToken ct = default);
    Task<IEnumerable<Usuario>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Usuario>> ObtenerActivosAsync(CancellationToken ct = default);
    Task AgregarAsync(Usuario usuario, CancellationToken ct = default);
    void Actualizar(Usuario usuario);
    void Eliminar(Usuario usuario);
    Task<bool> ExistePorCedulaAsync(string cedula, CancellationToken ct = default);
}
