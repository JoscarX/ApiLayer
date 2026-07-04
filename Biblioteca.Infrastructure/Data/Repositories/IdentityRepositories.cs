using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Data.Repositories;

internal sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == id, ct);

    public async Task<Usuario?> ObtenerPorCedulaAsync(string cedula, CancellationToken ct = default)
        => await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Cedula == cedula, ct);

    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Usuarios.Include(u => u.Rol).ToListAsync(ct);

    public async Task AgregarAsync(Usuario usuario, CancellationToken ct = default)
        => await _context.Usuarios.AddAsync(usuario, ct);

    public void Actualizar(Usuario usuario)
        => _context.Usuarios.Update(usuario);

    public void Eliminar(Usuario usuario)
        => _context.Usuarios.Remove(usuario);

    public async Task<IEnumerable<Usuario>> ObtenerActivosAsync(CancellationToken ct = default)
        => await _context.Usuarios.Include(u => u.Rol).Where(u => u.Activo).ToListAsync(ct);

    public async Task<bool> ExistePorCedulaAsync(string cedula, CancellationToken ct = default)
        => await _context.Usuarios.AnyAsync(u => u.Cedula == cedula, ct);
}

internal sealed class RolRepository : IRolRepository
{
    private readonly AppDbContext _context;

    public RolRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Rol?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Roles.FindAsync([id], ct);

    public async Task<Rol?> ObtenerPorNombreAsync(string nombre, CancellationToken ct = default)
        => await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == nombre, ct);

    public async Task<IEnumerable<Rol>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Roles.ToListAsync(ct);

    public async Task AgregarAsync(Rol rol, CancellationToken ct = default)
        => await _context.Roles.AddAsync(rol, ct);

    public void Actualizar(Rol rol)
        => _context.Roles.Update(rol);

    public void Eliminar(Rol rol)
        => _context.Roles.Remove(rol);

    public async Task<bool> ExistePorNombreAsync(string nombre, CancellationToken ct = default)
        => await _context.Roles.AnyAsync(r => r.Nombre == nombre, ct);
}
