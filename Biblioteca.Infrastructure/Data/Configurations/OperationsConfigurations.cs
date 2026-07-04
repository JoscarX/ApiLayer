using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Data.Configurations;

internal sealed class VisitanteConfiguration : IEntityTypeConfiguration<Visitante>
{
    public void Configure(EntityTypeBuilder<Visitante> builder)
    {
        builder.ToTable("Visitantes");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.NombreCompleto)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(v => v.Cedula)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.Telefono)
            .HasMaxLength(20);

        builder.Property(v => v.Direccion)
            .HasMaxLength(200);

        builder.Property(v => v.Correo)
            .HasMaxLength(120);

        builder.HasIndex(v => v.Cedula).IsUnique();
        builder.HasIndex(v => v.Correo).IsUnique().HasFilter("[Correo] IS NOT NULL");
    }
}

internal sealed class EntradaConfiguration : IEntityTypeConfiguration<Entrada>
{
    public void Configure(EntityTypeBuilder<Entrada> builder)
    {
        builder.ToTable("Entradas");
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Visitante)
            .WithMany(v => v.Entradas)
            .HasForeignKey(e => e.VisitanteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class PrestamoConfiguration : IEntityTypeConfiguration<Prestamo>
{
    public void Configure(EntityTypeBuilder<Prestamo> builder)
    {
        builder.ToTable("Prestamos");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Estado)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(p => p.Visitante)
            .WithMany(v => v.Prestamos)
            .HasForeignKey(p => p.VisitanteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Libro)
            .WithMany(l => l.Prestamos)
            .HasForeignKey(p => p.LibroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.ToTable("Ventas");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Subtotal).HasPrecision(18, 2);
        builder.Property(v => v.Descuento).HasPrecision(18, 2);
        builder.Property(v => v.Total).HasPrecision(18, 2);

        builder.Property(v => v.Estado)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(v => v.Visitante)
            .WithMany(v => v.Ventas)
            .HasForeignKey(v => v.VisitanteId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasMany(v => v.Detalles)
            .WithOne(d => d.Venta)
            .HasForeignKey(d => d.VentaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class DetalleVentaConfiguration : IEntityTypeConfiguration<DetalleVenta>
{
    public void Configure(EntityTypeBuilder<DetalleVenta> builder)
    {
        builder.ToTable("DetallesVenta");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Precio).HasPrecision(18, 2);
        builder.Property(d => d.Total).HasPrecision(18, 2);

        builder.HasOne(d => d.Libro)
            .WithMany(l => l.DetallesVenta)
            .HasForeignKey(d => d.LibroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

internal sealed class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
{
    public void Configure(EntityTypeBuilder<Auditoria> builder)
    {
        builder.ToTable("Auditorias");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Tabla)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Accion)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(a => a.Usuario)
            .WithMany()
            .HasForeignKey(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
