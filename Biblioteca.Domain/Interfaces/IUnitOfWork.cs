namespace Biblioteca.Domain.Interfaces;

/// <summary>
/// Unidad de trabajo que coordina transacciones entre múltiples repositorios.
/// Garantiza atomicidad en operaciones que afectan varias entidades.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IUsuarioRepository Usuarios { get; }
    IVisitanteRepository Visitantes { get; }
    ILibroRepository Libros { get; }
    ICategoriaRepository Categorias { get; }
    IAutorRepository Autores { get; }
    IEditorialRepository Editoriales { get; }
    IPrestamoRepository Prestamos { get; }
    IEntradaRepository Entradas { get; }
    IVentaRepository Ventas { get; }
    IAuditoriaRepository Auditorias { get; }
    IRolRepository Roles { get; }

    /// <summary>Persiste todos los cambios pendientes en la base de datos.</summary>
    Task<int> GuardarCambiosAsync(CancellationToken ct = default);

    /// <summary>Inicia una transacción explícita.</summary>
    Task IniciarTransaccionAsync(CancellationToken ct = default);

    /// <summary>Confirma la transacción activa.</summary>
    Task ConfirmarTransaccionAsync(CancellationToken ct = default);

    /// <summary>Revierte la transacción activa en caso de error.</summary>
    Task RevertirTransaccionAsync(CancellationToken ct = default);
}
