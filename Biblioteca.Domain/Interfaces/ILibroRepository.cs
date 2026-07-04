using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para la gestión del catálogo de libros.</summary>
public interface ILibroRepository
{
    Task<Libro?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<Libro?> ObtenerPorIdConCategoriasAsync(int id, CancellationToken ct = default);
    Task<Libro?> ObtenerPorIsbnAsync(string isbn, CancellationToken ct = default);
    Task<IEnumerable<Libro>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Libro>> ObtenerDisponiblesAsync(CancellationToken ct = default);
    Task<IEnumerable<Libro>> ObtenerTodosConCategoriasAsync(CancellationToken ct = default);
    Task<IEnumerable<Libro>> BuscarPorNombreAsync(string nombre, CancellationToken ct = default);
    Task AgregarAsync(Libro libro, CancellationToken ct = default);
    void Actualizar(Libro libro);
    void Eliminar(Libro libro);
    Task<bool> ExistePorIsbnAsync(string isbn, CancellationToken ct = default);
}
