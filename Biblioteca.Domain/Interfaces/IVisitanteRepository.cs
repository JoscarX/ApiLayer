using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

/// <summary>Repositorio para la gestión de visitantes de la biblioteca.</summary>
public interface IVisitanteRepository
{
    Task<Visitante?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<Visitante?> ObtenerPorCedulaAsync(string cedula, CancellationToken ct = default);
    Task<IEnumerable<Visitante>> ObtenerTodosAsync(CancellationToken ct = default);
    Task AgregarAsync(Visitante visitante, CancellationToken ct = default);
    void Actualizar(Visitante visitante);
    void Eliminar(Visitante visitante);
    Task<bool> ExistePorCedulaAsync(string cedula, CancellationToken ct = default);
    Task<bool> ExistePorCorreoAsync(string correo, CancellationToken ct = default);
}
