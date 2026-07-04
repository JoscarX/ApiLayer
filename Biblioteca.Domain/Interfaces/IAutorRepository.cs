using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para la gestión de autores.</summary>
public interface IAutorRepository
{
    Task<Autor?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Autor>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<IEnumerable<Autor>> BuscarPorNombreAsync(string nombre, CancellationToken ct = default);
    Task AgregarAsync(Autor autor, CancellationToken ct = default);
    void Actualizar(Autor autor);
    void Eliminar(Autor autor);
}
