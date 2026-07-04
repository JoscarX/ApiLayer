using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Data.Repositories;

internal sealed class AutorRepository : IAutorRepository
{
    private readonly AppDbContext _context;

    public AutorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Autor?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Autores.FindAsync([id], ct);

    public async Task<IEnumerable<Autor>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Autores.ToListAsync(ct);

    public async Task<IEnumerable<Autor>> BuscarPorNombreAsync(string nombre, CancellationToken ct = default)
        => await _context.Autores.Where(a => a.Nombre.Contains(nombre)).ToListAsync(ct);

    public async Task AgregarAsync(Autor autor, CancellationToken ct = default)
        => await _context.Autores.AddAsync(autor, ct);

    public void Actualizar(Autor autor)
        => _context.Autores.Update(autor);

    public void Eliminar(Autor autor)
        => _context.Autores.Remove(autor);
}

internal sealed class EditorialRepository : IEditorialRepository
{
    private readonly AppDbContext _context;

    public EditorialRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Editorial?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Editoriales.FindAsync([id], ct);

    public async Task<IEnumerable<Editorial>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Editoriales.ToListAsync(ct);

    public async Task<IEnumerable<Editorial>> BuscarPorNombreAsync(string nombre, CancellationToken ct = default)
        => await _context.Editoriales.Where(e => e.Nombre.Contains(nombre)).ToListAsync(ct);

    public async Task AgregarAsync(Editorial editorial, CancellationToken ct = default)
        => await _context.Editoriales.AddAsync(editorial, ct);

    public void Actualizar(Editorial editorial)
        => _context.Editoriales.Update(editorial);

    public void Eliminar(Editorial editorial)
        => _context.Editoriales.Remove(editorial);

    public async Task<bool> ExistePorNombreAsync(string nombre, CancellationToken ct = default)
        => await _context.Editoriales.AnyAsync(e => e.Nombre == nombre, ct);
}

internal sealed class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Categorias.FindAsync([id], ct);

    public async Task<Categoria?> ObtenerPorNombreAsync(string nombre, CancellationToken ct = default)
        => await _context.Categorias.FirstOrDefaultAsync(c => c.Nombre == nombre, ct);

    public async Task<IEnumerable<Categoria>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Categorias.ToListAsync(ct);

    public async Task AgregarAsync(Categoria categoria, CancellationToken ct = default)
        => await _context.Categorias.AddAsync(categoria, ct);

    public void Actualizar(Categoria categoria)
        => _context.Categorias.Update(categoria);

    public void Eliminar(Categoria categoria)
        => _context.Categorias.Remove(categoria);

    public async Task<bool> ExistePorNombreAsync(string nombre, CancellationToken ct = default)
        => await _context.Categorias.AnyAsync(c => c.Nombre == nombre, ct);
}

internal sealed class LibroRepository : ILibroRepository
{
    private readonly AppDbContext _context;

    public LibroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Libro?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Editorial)
            .FirstOrDefaultAsync(l => l.Id == id, ct);

    public async Task<Libro?> ObtenerPorIdConCategoriasAsync(int id, CancellationToken ct = default)
        => await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Editorial)
            .Include(l => l.Categorias)
            .FirstOrDefaultAsync(l => l.Id == id, ct);

    public async Task<Libro?> ObtenerPorIsbnAsync(string isbn, CancellationToken ct = default)
        => await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Editorial)
            .Include(l => l.Categorias)
            .FirstOrDefaultAsync(l => l.Isbn == isbn, ct);

    public async Task<IEnumerable<Libro>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Editorial)
            .ToListAsync(ct);

    public async Task<IEnumerable<Libro>> ObtenerDisponiblesAsync(CancellationToken ct = default)
        => await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Editorial)
            .Where(l => l.CantidadDisponible > 0)
            .ToListAsync(ct);

    public async Task<IEnumerable<Libro>> ObtenerTodosConCategoriasAsync(CancellationToken ct = default)
        => await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Editorial)
            .Include(l => l.Categorias)
            .ToListAsync(ct);

    public async Task<IEnumerable<Libro>> BuscarPorNombreAsync(string nombre, CancellationToken ct = default)
        => await _context.Libros
            .Include(l => l.Autor)
            .Include(l => l.Editorial)
            .Where(l => l.Nombre.Contains(nombre))
            .ToListAsync(ct);

    public async Task AgregarAsync(Libro libro, CancellationToken ct = default)
        => await _context.Libros.AddAsync(libro, ct);

    public void Actualizar(Libro libro)
        => _context.Libros.Update(libro);

    public void Eliminar(Libro libro)
        => _context.Libros.Remove(libro);

    public async Task<bool> ExistePorIsbnAsync(string isbn, CancellationToken ct = default)
        => await _context.Libros.AnyAsync(l => l.Isbn == isbn, ct);
}
