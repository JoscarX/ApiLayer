using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para la gestión de editoriales.</summary>
public interface IEditorialRepository
{
    Task<Editorial?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Editorial>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Editorial>> BuscarPorNombreAsync(string nombre, CancellationToken ct = default);
    Task AgregarAsync(Editorial editorial, CancellationToken ct = default);
    void Actualizar(Editorial editorial);
    void Eliminar(Editorial editorial);
    Task<bool> ExistePorNombreAsync(string nombre, CancellationToken ct = default);
}
