using System.Reflection;
using Biblioteca.Domain.Entities;
using Biblioteca.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
    private readonly AuditableEntityInterceptor _auditableEntityInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        AuditableEntityInterceptor auditableEntityInterceptor) 
        : base(options)
    {
        _auditableEntityInterceptor = auditableEntityInterceptor;
    }

    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Visitante> Visitantes => Set<Visitante>();
    public DbSet<Autor> Autores => Set<Autor>();
    public DbSet<Editorial> Editoriales => Set<Editorial>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Libro> Libros => Set<Libro>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();
    public DbSet<Entrada> Entradas => Set<Entrada>();
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<DetalleVenta> DetallesVenta => Set<DetalleVenta>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
