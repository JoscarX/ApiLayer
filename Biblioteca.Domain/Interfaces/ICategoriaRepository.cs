using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para la gestión de categorías literarias.</summary>
public interface ICategoriaRepository
{
    Task<Categoria?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<Categoria?> ObtenerPorNombreAsync(string nombre, CancellationToken ct = default);
    Task<IEnumerable<Categoria>> ObtenerTodosAsync(CancellationToken ct = default);
    Task AgregarAsync(Categoria categoria, CancellationToken ct = default);
    void Actualizar(Categoria categoria);
    void Eliminar(Categoria categoria);
    Task<bool> ExistePorNombreAsync(string nombre, CancellationToken ct = default);
}
