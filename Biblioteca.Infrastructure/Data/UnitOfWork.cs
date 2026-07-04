using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Biblioteca.Infrastructure.Data;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    public IUsuarioRepository Usuarios { get; }
    public IVisitanteRepository Visitantes { get; }
    public ILibroRepository Libros { get; }
    public ICategoriaRepository Categorias { get; }
    public IAutorRepository Autores { get; }
    public IEditorialRepository Editoriales { get; }
    public IPrestamoRepository Prestamos { get; }
    public IEntradaRepository Entradas { get; }
    public IVentaRepository Ventas { get; }
    public IAuditoriaRepository Auditorias { get; }
    public IRolRepository Roles { get; }

    public UnitOfWork(
        AppDbContext context,
        IUsuarioRepository usuarios,
        IVisitanteRepository visitantes,
        ILibroRepository libros,
        ICategoriaRepository categorias,
        IAutorRepository autores,
        IEditorialRepository editoriales,
        IPrestamoRepository prestamos,
        IEntradaRepository entradas,
        IVentaRepository ventas,
        IAuditoriaRepository auditorias,
        IRolRepository roles)
    {
        _context = context;
        Usuarios = usuarios;
        Visitantes = visitantes;
        Libros = libros;
        Categorias = categorias;
        Autores = autores;
        Editoriales = editoriales;
        Prestamos = prestamos;
        Entradas = entradas;
        Ventas = ventas;
        Auditorias = auditorias;
        Roles = roles;
    }

    public async Task<int> GuardarCambiosAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public async Task IniciarTransaccionAsync(CancellationToken ct = default)
    {
        if (_currentTransaction != null)
            return;

        _currentTransaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task ConfirmarTransaccionAsync(CancellationToken ct = default)
    {
        try
        {
            await GuardarCambiosAsync(ct);
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync(ct);
            }
        }
        catch
        {
            await RevertirTransaccionAsync(ct);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RevertirTransaccionAsync(CancellationToken ct = default)
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync(ct);
            }
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _context.Dispose();
    }
}
