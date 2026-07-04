using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Data.Repositories;

internal sealed class VisitanteRepository : IVisitanteRepository
{
    private readonly AppDbContext _context;

    public VisitanteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Visitante?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Visitantes.FindAsync([id], ct);

    public async Task<Visitante?> ObtenerPorCedulaAsync(string cedula, CancellationToken ct = default)
        => await _context.Visitantes.FirstOrDefaultAsync(v => v.Cedula == cedula, ct);

    public async Task<IEnumerable<Visitante>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Visitantes.ToListAsync(ct);

    public async Task AgregarAsync(Visitante visitante, CancellationToken ct = default)
        => await _context.Visitantes.AddAsync(visitante, ct);

    public void Actualizar(Visitante visitante)
        => _context.Visitantes.Update(visitante);

    public void Eliminar(Visitante visitante)
        => _context.Visitantes.Remove(visitante);

    public async Task<bool> ExistePorCedulaAsync(string cedula, CancellationToken ct = default)
        => await _context.Visitantes.AnyAsync(v => v.Cedula == cedula, ct);

    public async Task<bool> ExistePorCorreoAsync(string correo, CancellationToken ct = default)
        => await _context.Visitantes.AnyAsync(v => v.Correo == correo, ct);
}

internal sealed class PrestamoRepository : IPrestamoRepository
{
    private readonly AppDbContext _context;

    public PrestamoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Prestamo?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Prestamos
            .Include(p => p.Visitante)
            .Include(p => p.Libro)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IEnumerable<Prestamo>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Prestamos
            .Include(p => p.Visitante)
            .Include(p => p.Libro)
            .ToListAsync(ct);

    public async Task<IEnumerable<Prestamo>> ObtenerPorVisitanteAsync(int visitanteId, CancellationToken ct = default)
        => await _context.Prestamos
            .Include(p => p.Libro)
            .Where(p => p.VisitanteId == visitanteId)
            .ToListAsync(ct);

    public async Task<IEnumerable<Prestamo>> ObtenerPorLibroAsync(int libroId, CancellationToken ct = default)
        => await _context.Prestamos
            .Include(p => p.Visitante)
            .Where(p => p.LibroId == libroId)
            .ToListAsync(ct);

    public async Task<IEnumerable<Prestamo>> ObtenerPorEstadoAsync(EstadoPrestamo estado, CancellationToken ct = default)
        => await _context.Prestamos
            .Include(p => p.Visitante)
            .Include(p => p.Libro)
            .Where(p => p.Estado == estado)
            .ToListAsync(ct);

    public async Task<IEnumerable<Prestamo>> ObtenerVencidosAsync(DateTime fechaActual, CancellationToken ct = default)
        => await _context.Prestamos
            .Include(p => p.Visitante)
            .Include(p => p.Libro)
            .Where(p => p.Estado == EstadoPrestamo.Activo && p.FechaEntrega < fechaActual)
            .ToListAsync(ct);

    public async Task<IEnumerable<Prestamo>> ObtenerActivosAsync(CancellationToken ct = default)
        => await _context.Prestamos
            .Include(p => p.Visitante)
            .Include(p => p.Libro)
            .Where(p => p.Estado == EstadoPrestamo.Activo)
            .ToListAsync(ct);

    public async Task<bool> TienePrestamoActivoPorLibroAsync(int visitanteId, int libroId, CancellationToken ct = default)
        => await _context.Prestamos.AnyAsync(
            p => p.VisitanteId == visitanteId && 
                 p.LibroId == libroId && 
                 p.Estado == EstadoPrestamo.Activo, ct);

    public async Task AgregarAsync(Prestamo prestamo, CancellationToken ct = default)
        => await _context.Prestamos.AddAsync(prestamo, ct);

    public void Actualizar(Prestamo prestamo)
        => _context.Prestamos.Update(prestamo);
}

internal sealed class EntradaRepository : IEntradaRepository
{
    private readonly AppDbContext _context;

    public EntradaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Entrada?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Entradas
            .Include(e => e.Visitante)
            .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IEnumerable<Entrada>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Entradas
            .Include(e => e.Visitante)
            .ToListAsync(ct);

    public async Task<IEnumerable<Entrada>> ObtenerActivasAsync(CancellationToken ct = default)
        => await _context.Entradas
            .Include(e => e.Visitante)
            .Where(e => e.EstaActiva)
            .ToListAsync(ct);

    public async Task<IEnumerable<Entrada>> ObtenerPorVisitanteAsync(int visitanteId, CancellationToken ct = default)
        => await _context.Entradas
            .Where(e => e.VisitanteId == visitanteId)
            .ToListAsync(ct);

    public async Task<Entrada?> ObtenerEntradaActivaPorVisitanteAsync(int visitanteId, CancellationToken ct = default)
        => await _context.Entradas
            .FirstOrDefaultAsync(e => e.VisitanteId == visitanteId && e.EstaActiva, ct);

    public async Task AgregarAsync(Entrada entrada, CancellationToken ct = default)
        => await _context.Entradas.AddAsync(entrada, ct);

    public void Actualizar(Entrada entrada)
        => _context.Entradas.Update(entrada);
}

internal sealed class VentaRepository : IVentaRepository
{
    private readonly AppDbContext _context;

    public VentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Venta?> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => await _context.Ventas.FindAsync([id], ct);

    public async Task<Venta?> ObtenerPorIdConDetallesAsync(int id, CancellationToken ct = default)
        => await _context.Ventas
            .Include(v => v.Visitante)
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Libro)
            .FirstOrDefaultAsync(v => v.Id == id, ct);

    public async Task<IEnumerable<Venta>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Ventas.ToListAsync(ct);

    public async Task<IEnumerable<Venta>> ObtenerPorVisitanteAsync(int visitanteId, CancellationToken ct = default)
        => await _context.Ventas
            .Include(v => v.Detalles)
            .Where(v => v.VisitanteId == visitanteId)
            .ToListAsync(ct);

    public async Task<IEnumerable<Venta>> ObtenerConDetallesAsync(CancellationToken ct = default)
        => await _context.Ventas
            .Include(v => v.Visitante)
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Libro)
            .ToListAsync(ct);

    public async Task AgregarAsync(Venta venta, CancellationToken ct = default)
        => await _context.Ventas.AddAsync(venta, ct);

    public void Actualizar(Venta venta)
        => _context.Ventas.Update(venta);
}

internal sealed class AuditoriaRepository : IAuditoriaRepository
{
    private readonly AppDbContext _context;

    public AuditoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Auditoria>> ObtenerTodosAsync(CancellationToken ct = default)
        => await _context.Auditorias.Include(a => a.Usuario).OrderByDescending(a => a.Fecha).ToListAsync(ct);

    public async Task<IEnumerable<Auditoria>> ObtenerPorTablaAsync(string tabla, CancellationToken ct = default)
        => await _context.Auditorias
            .Include(a => a.Usuario)
            .Where(a => a.Tabla == tabla)
            .OrderByDescending(a => a.Fecha)
            .ToListAsync(ct);

    public async Task<IEnumerable<Auditoria>> ObtenerPorUsuarioAsync(int usuarioId, CancellationToken ct = default)
        => await _context.Auditorias
            .Include(a => a.Usuario)
            .Where(a => a.UsuarioId == usuarioId)
            .OrderByDescending(a => a.Fecha)
            .ToListAsync(ct);

    public async Task<IEnumerable<Auditoria>> ObtenerPorAccionAsync(AccionAuditoria accion, CancellationToken ct = default)
        => await _context.Auditorias
            .Include(a => a.Usuario)
            .Where(a => a.Accion == accion)
            .OrderByDescending(a => a.Fecha)
            .ToListAsync(ct);

    public async Task<IEnumerable<Auditoria>> ObtenerPorRangoFechaAsync(DateTime desde, DateTime hasta, CancellationToken ct = default)
        => await _context.Auditorias
            .Include(a => a.Usuario)
            .Where(a => a.Fecha >= desde && a.Fecha <= hasta)
            .OrderByDescending(a => a.Fecha)
            .ToListAsync(ct);

    public async Task AgregarAsync(Auditoria auditoria, CancellationToken ct = default)
        => await _context.Auditorias.AddAsync(auditoria, ct);
}
